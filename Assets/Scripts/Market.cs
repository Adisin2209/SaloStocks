using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Market : MonoBehaviour
{
    public static Market Instance;
    
    
    public List<Stock> stocks = new();
    bool paused = false;
    float e = 1.5f; 

    void Start()
    { 
       Instance = this;
       
       Stocks.Init();
       Events.Init();
       stocks = Stocks.All.ToList();
       
       
      // e =  WeatherTime.Instance.waitTime;
       
    }

    // Update is called once per frame
    void Update()
    {
        Player.Instance.calcAmount();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            //startNextRound();
            Player.Instance.addStock(Stocks.Salo);
            Debug.Log("Salo Bought");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
           // Player.Instance.addStock(Stocks.Vyshivanka);
           SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Player.Instance.addStock(Stocks.Vyshivanka);
            LoadGame();
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!paused)
            {
                paused = true;
                WeatherTime.Instance.waitTime = 999999;
            }
            else
            {
                paused = false;
                WeatherTime.Instance.waitTime = e;
            }
            
            
        }
    }

    public void startNextDay()
    {
        stocks.Clear();
        stocks = Stocks.All.ToList();
        
        distributeRandomToAllStocks(-0.02f,0.02f);
        SaveGame();
       // Debug.Log("Vyshivanka price: "+Stocks.Vyshyvanka.history[WeatherTime.Instance.daysPassed]);
        //Events.fireRandomEvent();
        //StartCoroutine(HELPER_fireEventDelay());
        // Player.Instance.calcAmount();
    }

    public void distributeRandomToAllStocks(float min , float max)
    {
        
        foreach (var s in Stocks.All)
        {
            float impact = Random.Range(min, max);
            s.price += s.price*impact;
            Debug.Log(s.name + " : " + impact+" at: "+WeatherTime.Instance.currentTime);
        }
    }

    #region SAVE SYSTEM IMPLEMENT

    public void SaveGame()
    {
        SaveSystem.SaveData();
        Debug.Log("saved");
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadData();
        
        Player.Instance.freeBalance = data.cash;
        WeatherTime.Instance.daysPassed = data.currentDay;
        WeatherTime.Instance.currentTime = data.currentHour;
        
        // Restore stock prices
        foreach (var savedStock in data.stockPrices)
        {
            if (Stocks.All.FirstOrDefault(s => s.name == savedStock.name) is { } existingStock)
            {
                existingStock.price = savedStock.price;
                existingStock.history = new List<float>(savedStock.history);;
            }
        }

        // Restore player portfolio
        Player.Instance.stocksInWallet.Clear();
        foreach (var saved in data.portfolio)
        {
            if (Stocks.All.FirstOrDefault(s => s.name == saved.name) is { } stock)
            {
                Player.Instance.stocksInWallet.Add(new Player.stockInWallet(stock, saved.amount));
            }
        }
        Stocks.updateStockHistory();
        Debug.Log("loaded");
    }
    
    

    #endregion
}
