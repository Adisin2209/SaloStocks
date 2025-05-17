using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stock
{
    [ReadOnly]
    public string name;
    public float price;
    public List<float> history = new();

    public Stock(string name, float price)
    {
        this.name = name;
        this.price = price;
     //   this.history = new List<float>();
    }
    
    public void UpdateHistory(int day)
    {
        // Falls Tag negativ ist, überspringen
        if (day < 0) return;

        // Liste auf benötigte Länge bringen, ggf. mit dem letzten bekannten Preis auffüllen
        while (history.Count <= day)
        {
            float lastKnownPrice = history.Count > 0 ? history[^1] : price;
            history.Add(lastKnownPrice); // statt 0: realistischeren Preis verwenden
        }

        // Preis für den Tag setzen (ggf. überschreiben)
        history[day] = price;
    }


    public float GetPriceChangeRaw(int daysAgo)
    {
        int currentDay = WeatherTime.Instance.daysPassed-1;
        int pastDay = currentDay-daysAgo;
        
        if (pastDay < 0 || history.Count <= currentDay || history.Count <= pastDay)
            return 0f;
        
        return history[currentDay]-history[pastDay];
    }
    public float GetPriceChangePercent(int daysAgo)
    {
        int currentDay = WeatherTime.Instance.daysPassed - 1;
        int pastDay = currentDay - daysAgo;

        if (pastDay < 0 || history.Count <= currentDay || history.Count <= pastDay)
            return 0f;

        float pastPrice = history[pastDay];
        float currentPrice = history[currentDay];

        if (pastPrice == 0) return 0f;

        return (currentPrice - pastPrice) / pastPrice * 100f;
    }
    
}




public static class Stocks 
{
    private static Dictionary<string, Stock> _registry = new();
    
    
    
    #region CREATING_STATIC_STOCKS
    public static Stock Salo => _registry["Salo"];
    public static Stock Vyshyvanka => _registry["Vyshyvanka"];
    public static Stock Horilka => _registry["Horilka"];
    #endregion
    
    public static void Init()
    {
        if (_registry.Count > 0) return; // Nur einmal initialisieren
        
        
        #region STOCKREGISTRY
        _registry["Salo"] = new Stock("Salo", 100f);
        _registry["Vyshyvanka"] = new Stock("Vyshyvanka", 500f);
        _registry["Horilka"] = new Stock("Horilka", 300f);
        #endregion
    }
    
    public static Stock GetStockByKey(string key)
    {
        string sanitizedKey = key.Replace(" ", ""); // entfernt alle Leerzeichen

        if (_registry.TryGetValue(sanitizedKey, out var stock))
        {
            Debug.Log("Found " + sanitizedKey + " stock");
            return stock;
        }

        Debug.LogWarning($"Stock '{sanitizedKey}' not found in registry.");
        return null;
    }

    public static void updateStockHistory()
    {
        int day = WeatherTime.Instance.daysPassed;
        
        foreach (var s in All)
        {
            s.UpdateHistory(day-1);
        }
    }

    public static IEnumerable<Stock> All => _registry.Values;
    public static IEnumerable<string> AllKeys => _registry.Keys;
}
