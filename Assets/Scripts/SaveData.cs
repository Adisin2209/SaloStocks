using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedStock
{
    public string name;
    public string key;
    public float price;
    public List<float> history;
}

[System.Serializable]
public class SavedPortfolioStock
{
    public string name;
    public int amount;
}

[System.Serializable]
public class SaveData
{
    public int currentDay;
    public int currentHour;
    public float cash;
    
    public List<SavedStock> stockPrices;
    public List<SavedPortfolioStock> portfolio;
    
    public List<Event> events;


    public SaveData()
    {
        currentDay = WeatherTime.Instance.daysPassed;
        cash = Player.Instance.freeBalance;
        currentHour = WeatherTime.Instance.currentTime;
        
        stockPrices = new List<SavedStock>();
        events = Events.eventList;
        foreach (var stock in Stocks.All)
        {
            string key = stock.name.Replace(" ", "");
            stockPrices.Add(new SavedStock
            {
                name = stock.name,
                key = key,
                price = stock.price,
                history = new List<float>(stock.history)
            });
        }
        
        //foreach (var stock in Stocks.All)
        //{
            //stockPrices.Add(new SavedStock { name = stock.name, price = stock.price, history = new List<float>(stock.history) });
        //}

        portfolio = new List<SavedPortfolioStock>();
        foreach (var s in Player.Instance.stocksInWallet)
        {
            portfolio.Add(new SavedPortfolioStock { name = s.stock.name, amount = s.amount });
        }

    }
}
