using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Stock
{
    [ReadOnly]
    public string name;
    public float price;
    public List<float> history = new();
    public float initialPrice;

    public Stock(string name, float price)
    {
        this.name = name;
        this.price = price;
        initialPrice = price;
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
    public static Stock Semki => _registry["Semki"];
    public static Stock CossacksTourismETF => _registry["CossacksTourismETF"];
    public static Stock Varenyky => _registry["Varenyky"];
    public static Stock Kvass => _registry["Kvass"];
    public static Stock RussianTankScrap => _registry["RussianTankScrap"];
    public static Stock ChernobylAdventurePark => _registry["ChernobylAdventurePark"];
    public static Stock ThermalScopeSelfies => _registry["ThermalScopeSelfies"];
    public static Stock OrthoCoin => _registry["OrthoCoin"];
    public static Stock Buckwheat => _registry["Buckwheat"];
    public static Stock SunflowerOil => _registry["SunflowerOil"];
    #endregion
    
    public static void Init()
    {
        if (_registry.Count > 0) return; // Nur einmal initialisieren
        
        
        #region STOCKREGISTRY
        _registry["Salo"] = new Stock("Salo", 150f);
        _registry["Vyshyvanka"] = new Stock("Vyshyvanka", 400f);
        _registry["Horilka"] = new Stock("Horilka", 220f);
        _registry["Semki"] = new Stock("Semki", 70f);
        _registry["CossacksTourismETF"] = new Stock("Cossacks Tourism ETF", 600f);
        _registry["Varenyky"] = new Stock("Varenyky", 200f);
        _registry["Kvass"] = new Stock("Kvass", 90f);
        _registry["RussianTankScrap"] = new Stock("Russian Tank Scrap", 300f);
        _registry["ChernobylAdventurePark"] = new Stock("Chernobyl Adventure Park", 670f);
        _registry["ThermalScopeSelfies"] = new Stock("Thermal Scope Selfies", 420f);
        _registry["OrthoCoin"] = new Stock("Ortho Coin", 500f);
        _registry["Buckwheat"] = new Stock("Buckwheat", 120f);
        _registry["SunflowerOil"] = new Stock("Sunflower Oil", 160f);
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
    
    public static void AddStock(string name, float price)
    {
        string key = name.Replace(" ", "");

        if (_registry.ContainsKey(key))
        {
            Debug.LogWarning($"Stock '{name}' already exists in registry.");
            return;
        }

        _registry[key] = new Stock(name, price);
        Debug.Log($"Stock '{name}' with price {price} added to registry. With key {key}");
        
        Init();
        Market.Instance.stocks = Stocks.All.ToList(); 
        UI_Handler.Instance.populateStocksUI();
    }

    
    
    public static void ClearRegistry()
    {
        _registry.Clear();
    }


    public static IEnumerable<Stock> All => _registry.Values;
    public static IEnumerable<string> AllKeys => _registry.Keys;
}
