using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class WeatherTime : MonoBehaviour
{
    public static WeatherTime Instance;
    
    [Header("|====DEBUG====|")]
    [ReadOnly]
    public int daysPassed = 0;
    [ReadOnly]
    public int currentTime;
    
    
    [Header("Variables")]
    public float waitTime = 1;
    public float dayLength = 2;
    public int nightStart;
    public int dayStart;
    
    [Header("Components")]
    public Light2D globalLight;
    public GameObject Day;
    public GameObject Night;
    
// Intern: Eventsystem
    private int eventHour;
    private bool eventFiredToday;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        updateTimeFX();
        StartCoroutine(TimeClock());
    }

    // Update is called once per frame
    void Update()
    {
        updateTimeFX();
    }

    IEnumerator TimeClock()
    {
        //Start new Day
        
        eventHour = Random.Range(7, 19); // 7 bis 18
        eventFiredToday = false;
        
        
        currentTime = 0;
        
        daysPassed++;
        while (currentTime <= dayLength-1)
        {
            
            if (!eventFiredToday && currentTime == eventHour)
            {
                Events.fireRandomEvent();
                Stocks.updateStockHistory();
                Market.Instance.SaveGame();
                eventFiredToday = true;
            }
            
            
            yield return new WaitForSeconds(waitTime);
           // Market.Instance.distributeRandomToAllStocks(-0.02f,0.02f); //random hourly change in stocks
            Stocks.updateStockHistory();
            currentTime++;
        }
        
        StartCoroutine(TimeClock());
        Market.Instance.startNextDay();
        
    }

    void updateTimeFX()
    {
        if (currentTime == dayStart)
        {
            Night.SetActive(false);
            Day.SetActive(true);
            globalLight.intensity = 1;
        }else if (currentTime > nightStart-1)
        {
            Night.SetActive(true);
            Day.SetActive(false);
            globalLight.intensity = 0;
        }
    }
}
