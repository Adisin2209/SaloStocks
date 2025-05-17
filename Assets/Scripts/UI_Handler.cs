using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public static UI_Handler Instance;
    
    public GameObject GeneralInfoPanel;
    public TMP_Text currentDay;
    public TMP_Text playerBalance;
    public TMP_Text portfolioBalance;
    public TMP_Text totalBalance;
    public GameObject stocksSpawn;
    public GameObject stockButtonPrefab;

    [Header("Stockmarket Info")]
    public GameObject stockMarketWindow;
    [ReadOnly]
    public bool stockMarketOpen = true;
    
    [Header("Notifications")]
    public GameObject mouseNotificationsPos;
    public GameObject mouseNotifPrefab;
    [Header("Event Notifications")]
    public GameObject eventNotifications;
    public TMP_Text eventNameText;
    public TMP_Text eventDescriptionText;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        pullGeneralInfoPanel();
        
    }

    void pullGeneralInfoPanel()
    {
        currentDay.text = "Day: "+WeatherTime.Instance.daysPassed.ToString() + "  "+WeatherTime.Instance.currentTime.ToString()+":00";
        playerBalance.text = "Cash: "+ Player.Instance.freeBalance.ToString("F2")+" HRN";
        portfolioBalance.text = "Portfolio: "+ Player.Instance.portfolioBalance.ToString("F2")+" HRN";
        float totalBal =  Player.Instance.freeBalance + Player.Instance.portfolioBalance;
        totalBalance.text = "Total: "+ totalBal.ToString("F2")+" HRN";
    }

    public void stockMarketButton()
    {
        if (stockMarketOpen)
        {
            stockMarketOpen = false;
            stockMarketWindow.SetActive(false);
        }
        else if (!stockMarketOpen)
        {
            populateStocksUI();
            stockMarketOpen = true;
            stockMarketWindow.SetActive(true);
        }
    }

    public void populateStocksUI()
    {
        foreach (var s in Stocks.All)
        {
            bool alreadyExists = false;

            foreach (Transform child in stocksSpawn.transform)
            {
                UI_Stock uiStock = child.GetComponent<UI_Stock>();
                if (uiStock != null && uiStock.selectedStockKey == s.name)
                {
                    alreadyExists = true;
                    break;
                }
            }

            if (!alreadyExists)
            {
                GameObject stockButton = Instantiate(stockButtonPrefab, stocksSpawn.transform);
                stockButton.GetComponent<UI_Stock>().selectedStockKey = s.name;
            }
        }
    }

    public void createMouseNotification(string text)
    {
        
        GameObject mouseNotification = Instantiate(mouseNotifPrefab, mouseNotificationsPos.transform);
        mouseNotification.GetComponent<TMP_Text>().text = text;
    }

    

}
