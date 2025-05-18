using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public string name;
    public string description;
    public Stock stock;
    public float impact;
    public float probability;

    public Event(string name, string description, Stock stock, float impact, float probability)
    {
        this.name = name;
        this.description = description;
        this.stock = stock;
        this.impact = impact / 100;
        this.probability = probability;
    }
}

public static class Events
{
    private static List<Event> eventList = new();

    #region CREATING_EVENTS
    public static Event SaloSpoilage { get; private set; }
    public static Event VyshyvankaFashionTrend { get; private set; }
    #endregion

    public static void Init()
    {
        eventList.Clear();


        #region INIT_EVENTS
SaloSpoilage = new Event("Salo Spoilage", "Salo was stored wrong and now smells terrible!", Stocks.Salo, -25f, 150);
        eventList.Add(SaloSpoilage);

        VyshyvankaFashionTrend = new Event("Vyshyvanka Fashion Trend", "Vyshyvanka becomes the new hipster must-have!", Stocks.Vyshyvanka, 40f, 300);
        eventList.Add(VyshyvankaFashionTrend);
        #endregion
    }

    public static void fireEvent(Event e)
    {
        Debug.Log(e.name + ": " + "\n" + e.description);
        e.stock.price += e.stock.price * e.impact;
        UI_Handler.Instance.eventNameText.text ="Day "+ WeatherTime.Instance.daysPassed+" "+WeatherTime.Instance.currentTime+":00 "+": "+e.name ;
        if (e.impact < 0)
        {
            //NEGATIVER IMPACT MINUS
            UI_Handler.Instance.eventDescriptionText.text = e.description + "\n<color=#FF0000>" + e.stock.name + " " + (e.impact * 100f).ToString("F2") + "%</color>";
        }
        else
        {
            UI_Handler.Instance.eventDescriptionText.text = e.description + "\n<color=#02fa02>" + e.stock.name + " +" + (e.impact * 100f).ToString("F2") + "%</color>";
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