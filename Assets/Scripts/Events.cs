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
    public Action customEventAction;

    public Event(string name, string description, List<StockImpact> affectedStocks, float probability
    )
    {
        this.name = name;
        this.description = description;
        this.affectedStocks = affectedStocks;
        this.probability = probability;

        this.customEventAction = null; // keine zusätzliche Logik per Standard
    }

    public Event(string name, string description, List<StockImpact> affectedStocks, float probability,
        Action customEventAction)
        : this(name, description, affectedStocks, probability)
    {
        this.customEventAction = customEventAction;
    }
}

[System.Serializable]
public static class Events
{
    public static List<Event> eventList = new();

    #region CREATING_EVENTS

    #region generic events

    public static Event SaloSpoilage { get; private set; }
    public static Event VyshyvankaFashionTrend { get; private set; }
    public static Event NationalBaconFestival { get; private set; }
    public static Event InternationalHorilkaDay { get; private set; }
    public static Event RecordFootballSeason { get; private set; }
    public static Event CossackNetflixSeriesReleased { get; private set; }
    public static Event VarenykyWorldRecord { get; private set; }
    public static Event SummerHeatwave { get; private set; }
    public static Event EuropeanScrapArtTrend { get; private set; }
    public static Event InfluencerThermalTrend { get; private set; }
    public static Event IconMiracle { get; private set; }
    public static Event BuckwheatDietTrend { get; private set; }
    public static Event GlobalOilShortage { get; private set; }
    public static Event EmbroideryScandal { get; private set; }
    public static Event CounterfeitHorilkaCrisis { get; private set; }
    public static Event PoorSunflowerHarvest { get; private set; }
    public static Event HistoricalControversy { get; private set; }
    public static Event FillingShortage { get; private set; }
    public static Event KvassContaminationPanic { get; private set; }
    public static Event MetalGlutCrisis { get; private set; }
    public static Event MutantWildlifeRumors { get; private set; }
    public static Event ThermalPrivacyScandal { get; private set; }
    public static Event ReligiousCorruptionScandal { get; private set; }

    #endregion


    #region special events

    public static Event Stalker2Released { get; private set; }

    #endregion


    #region continuation events

    public static Event OrthodoxyRepresentedInStalker2 { get; private set; }

    #endregion

    #endregion

    public static void Init()
    {
        //eventList.Clear();


        #region INIT_EVENTS

        #region GENERIC EVENTS

        NationalBaconFestival = new Event(
            "National Bacon Festival",
            "Salo lovers unite! Salo is king today. Unfortunately, young hipsters avoid Varenyky—apparently carbs are now the enemy.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Salo, 35f),
                new StockImpact(Stocks.Varenyky, -10f)
            },
            10f);
        eventList.Add(NationalBaconFestival);


        VyshyvankaFashionTrend = new Event(
            "Vyshyvanka Fashion Trend",
            "Vyshyvanka becomes the new hipster must-have! Even Salo slightly benefits from this trendy wave.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Vyshyvanka, 35f),
                new StockImpact(Stocks.Salo, 10f)
            },
            10f);
        eventList.Add(VyshyvankaFashionTrend);

        InternationalHorilkaDay = new Event(
            "International Horilka Day",
            "Horilka exports explode worldwide. Orthodox babushkas aren't amused though—something about morals.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Horilka, 30f),
                new StockImpact(Stocks.OrthoCoin, -10f)
            },
            9f
        );
        eventList.Add(InternationalHorilkaDay);

        RecordFootballSeason = new Event(
            "Record Football Season",
            "Ukraine in finals! Semki and Kvass sales soar nationwide, but tourism falls slightly as everyone's glued to the TV.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Semki, 40f),
                new StockImpact(Stocks.Kvass, 15f),
                new StockImpact(Stocks.CossacksTourismETF, -10f)
            },
            7f);
        eventList.Add(RecordFootballSeason);

        CossackNetflixSeriesReleased = new Event(
            "Cossack Netflix Series Released",
            "A Netflix hit boosts Cossack tourism, but westerners confuse authentic Vyshyvankas due to poor costume design. Typical...",
            new List<StockImpact>
            {
                new StockImpact(Stocks.CossacksTourismETF, 35f),
                new StockImpact(Stocks.Vyshyvanka, -15f)
            },
            6f);
        eventList.Add(CossackNetflixSeriesReleased);

        VarenykyWorldRecord = new Event(
            "Varenyky Eating World Record",
            "A new dumpling-eating hero sparks nationwide excitement. Health-food Buckwheat trends downward temporarily.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Varenyky, 30f),
                new StockImpact(Stocks.Buckwheat, -15f)
            },
            8f);
        eventList.Add(VarenykyWorldRecord);

        SummerHeatwave = new Event(
            "Summer Heatwave",
            "Scorching heat makes Kvass humanity's greatest invention. Sales explode.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Kvass, 35f)
            },
            8f);
        eventList.Add(SummerHeatwave);

        EuropeanScrapArtTrend = new Event(
            "European Scrap Art Trend",
            "European artists love Russian tank scrap. Finally they're good for something!",
            new List<StockImpact>
            {
                new StockImpact(Stocks.RussianTankScrap, 40f)
            },
            6f);
        eventList.Add(EuropeanScrapArtTrend);

        InfluencerThermalTrend = new Event(
            "Influencer Thermal Trend",
            "Influencers make Thermal Scope Selfies cool overnight. Heat is literally trending!",
            new List<StockImpact>
            {
                new StockImpact(Stocks.ThermalScopeSelfies, 35f)
            },
            5f);
        eventList.Add(InfluencerThermalTrend);

        IconMiracle = new Event(
            "Miracle Reported in Orthodox Church",
            "A miracle boosts faith enormously. Naturally, OrthoCoin is booming!",
            new List<StockImpact>
            {
                new StockImpact(Stocks.OrthoCoin, 45f)
            },
            4f,
            (() =>
            {
                IconMiracle.probability -= IconMiracle.probability * (1 / 10);
            }));
        eventList.Add(IconMiracle);

        BuckwheatDietTrend = new Event(
            "Buckwheat Diet Trend",
            "Ukrainian buckwheat diet wins global attention. Salty Salo loses popularity among hipsters.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.Buckwheat, 35f),
                new StockImpact(Stocks.Salo, -10f)
            },
            6f);
        eventList.Add(BuckwheatDietTrend);

        GlobalOilShortage = new Event(
            "Global Cooking Oil Shortage",
            "World discovers Ukrainian sunflower oil. Semki snacks sales drop due to high seed prices. Global demands ruin snack time!",
            new List<StockImpact>
            {
                new StockImpact(Stocks.SunflowerOil, 40f),
                new StockImpact(Stocks.Semki, -15f)
            },
            5f);
        eventList.Add(GlobalOilShortage);

        #endregion


        #region SPECIAL EVENTS

        Stalker2Released = new Event(
            "S.T.A.L.K.E.R. 2 Released!",
            "Fans invade Chernobyl Adventure Park! Unfortunately, local religious figures disapprove strongly.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.ChernobylAdventurePark, 70f),
                new StockImpact(Stocks.ThermalScopeSelfies, 25f),
                new StockImpact(Stocks.OrthoCoin, -10f)
            },
            3f,
            () =>
            {
                Debug.Log("STALKER 2 IST DRAUSSEN!");
                Events.Stalker2Released.probability = 0f;
                Events.OrthodoxyRepresentedInStalker2.probability = 6f;
            });
        eventList.Add(Stalker2Released);

        #endregion


        #region CONTINUATION EVENTS

        OrthodoxyRepresentedInStalker2 = new Event(
            "Orthodoxy Represented in S.T.A.L.L.E.R 2",
            "Game packed with religious Icons and Orthodox Iconography. Religious figures take back criticism and endorse the Game.",
            new List<StockImpact>
            {
                new StockImpact(Stocks.ChernobylAdventurePark, 15f),
                new StockImpact(Stocks.OrthoCoin, 30f)
            },
            0f,
            () => { Events.OrthodoxyRepresentedInStalker2.probability = 0f; });
        eventList.Add(OrthodoxyRepresentedInStalker2);

        #endregion

        #endregion
    }

    public static void addEvent(string name, string description, float probability, List<StockImpact> affectedStocks)
    {
        eventList.Add(new Event(name, description, affectedStocks, probability));
    }

    public static void fireEvent(Event e)
    {
        Debug.Log(e.name + ": " + "\n" + e.description);
        //  e.stock.price += e.stock.price * e.impact;

        UI_Handler.Instance.eventNameText.text = "Day " + WeatherTime.Instance.daysPassed + " " +
                                                 WeatherTime.Instance.currentTime + ":00 " + ": " + e.name;
        UI_Handler.Instance.eventDescriptionText.text = e.description;

        foreach (var pair in e.affectedStocks)
        {
            Stock stock = pair.stock;
            float impact = pair.impact;

            stock.price += stock.price * impact;
            e.customEventAction?.Invoke();

            if (impact < 0)
            {
                //NEGATIVER IMPACT MINUS
                UI_Handler.Instance.eventDescriptionText.text += "\n<color=#FF0000>" + stock.name + " " +
                                                                 (impact * 100f).ToString("F2") + "%</color>";
            }
            else
            {
                UI_Handler.Instance.eventDescriptionText.text += "\n<color=#34D900>" + stock.name + " +" +
                                                                 (impact * 100f).ToString("F2") + "%</color>";
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