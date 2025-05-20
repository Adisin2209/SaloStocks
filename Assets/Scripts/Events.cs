using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class StockImpact
{
    public Stock stock;
    public float impact;

    public StockImpact(Stock stock, float impact)
    {
        this.stock = stock;
        this.impact = impact / 100f; // Prozent zu Faktor
    }
}
[System.Serializable]
public class Event
{
    public string name;
    public string description;
    public List<StockImpact> affectedStocks;
    public float probability;

    public Event(string name, string description, List<StockImpact> affectedStocks, float probability)
    {
        this.name = name;
        this.description = description;
        this.affectedStocks = affectedStocks;
        this.probability = probability;
    }
}

[System.Serializable]
public static class Events
{
    public static List<Event> eventList = new();

    #region CREATING_EVENTS
    public static Event SaloSpoilage { get; private set; }
    public static Event VyshyvankaFashionTrend { get; private set; }
    //public static Event HesmoDihhSpermrace { get; private set; }
    #endregion

    public static void Init()
    {
        //eventList.Clear();


        #region INIT_EVENTS
        
        SaloSpoilage = new Event(
            "Salo Spoilage",
            "Salo was stored wrong and now smells terrible!",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Salo, -25f),
                new StockImpact(Stocks.Vyshyvanka, -10f)
            },
            10
        );
        eventList.Add(SaloSpoilage);

        
        VyshyvankaFashionTrend = new Event(
            "Vyshyvanka Fashion Trend",
            "Vyshyvanka becomes the new hipster must-have!",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Vyshyvanka, 40f),
                new StockImpact(Stocks.Salo, 10f) // Beispiel: Auch Salo profitiert leicht
            },
            10
        );
        eventList.Add(VyshyvankaFashionTrend);
        
        

        //HesmoDihhSpermrace = new Event("Hesmo Dihh Spermrace", "Upcoming Spermrace by Hesmos Dihhh", Stocks. HesmoDihh,  300f,  1000);
        //eventList.Add(HesmoDihhSpermrace);
        
        
        #endregion
    }

    public static void addEvent(string name,string description ,float probability, List<StockImpact> affectedStocks)
    {
        eventList.Add(new Event(name,description,affectedStocks,probability));
    }

    public static void fireEvent(Event e)
    {
        Debug.Log(e.name + ": " + "\n" + e.description);
      //  e.stock.price += e.stock.price * e.impact;
        
        UI_Handler.Instance.eventNameText.text ="Day "+ WeatherTime.Instance.daysPassed+" "+WeatherTime.Instance.currentTime+":00 "+": "+e.name;
        UI_Handler.Instance.eventDescriptionText.text = e.description;

        foreach (var pair in e.affectedStocks)
        {
            Stock stock = pair.stock;
            float impact = pair.impact;
            
            stock.price += stock.price * impact;
            
            if (impact < 0)
            {
                //NEGATIVER IMPACT MINUS
                UI_Handler.Instance.eventDescriptionText.text += "\n<color=#FF0000>" + stock.name + " " + (impact * 100f).ToString("F2") + "%</color>";
            }
            else
            {
                UI_Handler.Instance.eventDescriptionText.text += "\n<color=#34D900>" + stock.name + " +" + (impact * 100f).ToString("F2") + "%</color>";
            }
            
        }
        
        
        UI_Handler.Instance.eventNotifications.GetComponent<Animation>().Stop();
        UI_Handler.Instance.eventNotifications.GetComponent<Animation>().Play();
    }

    public static void fireRandomEvent()
    {
        float totalWeight = 0f;
        foreach (Event ev in eventList)
        {
            totalWeight += ev.probability;
        }

        float rng = Random.value * totalWeight;
        int chooseNumber = 0;
        float add = eventList[0].probability;
        while (rng > add && chooseNumber < eventList.Count - 1)
        {
            chooseNumber++;
            add += eventList[chooseNumber].probability;
        }

        if (WeatherTime.Instance.daysPassed > 1)
        {
            fireEvent(eventList[chooseNumber]);
        }
        
    }
    
    
    
    
}