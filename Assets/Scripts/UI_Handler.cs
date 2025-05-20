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
    
    [Header("Detailed Stock Info")]
    public Stock CurrentStock;
    public GameObject detailedStockInfoWindow;
    public TMP_Text detailedStockNameText;
    public TMP_Text detailedStockPriceText;
    public TMP_Text detailedStock24HText;
    public TMP_Text detailedStock7DText;
    public TMP_Text detailedStockALLText;
    public TMP_Text detailedStockAMOUNTText;
    public Image stockIcon;
    
    
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
        //CurrentStock = Stocks.Salo;
        pullGeneralInfoPanel();
        detailedStockButton();
    }

    void pullGeneralInfoPanel()
    {
        currentDay.text = "Day: "+WeatherTime.Instance.daysPassed.ToString() + "\n"+WeatherTime.Instance.currentTime.ToString()+":00";
        playerBalance.text = Player.Instance.freeBalance.ToString("F2")+" HRN";
        portfolioBalance.text = Player.Instance.portfolioBalance.ToString("F2")+" HRN";
        float totalBal =  Player.Instance.freeBalance + Player.Instance.portfolioBalance;
        totalBalance.text = "Total: "+ totalBal.ToString("F2")+" HRN";
    }

    public void stockMarketButton()
    {
        if (stockMarketWindow.activeSelf)
        {
            stockMarketOpen = false;
            stockMarketWindow.SetActive(false);
        }
        else if (!stockMarketWindow.activeSelf)
        {
            populateStocksUI();
            stockMarketOpen = true;
            stockMarketWindow.SetActive(true);
        }
    }

    public void populateStocksUI()
    {
        
        
        // Entferne alle alten Buttons
        foreach (Transform child in stocksSpawn.transform)
        {
            Destroy(child.gameObject);
        }

        // Jetzt frische Buttons erzeugen
        foreach (var s in Market.Instance.stocks)
        {
            GameObject stockButton = Instantiate(stockButtonPrefab, stocksSpawn.transform);
            stockButton.GetComponent<UI_Stock>().selectedStockKey = s.name;
        }
    }

    public void createMouseNotification(string text)
    {
        
        GameObject mouseNotification = Instantiate(mouseNotifPrefab, mouseNotificationsPos.transform);
        mouseNotification.GetComponent<TMP_Text>().text = text;
    }

    public void detailedStockButton()
    {
        if (WeatherTime.Instance.daysPassed <= 1)
        {
            detailedStock24HText.text = "NO DATA YET";
            detailedStock7DText.text = "NO DATA YET";
            detailedStockALLText.text = "NO DATA YET";
            
        }
        
        
        if (CurrentStock != null)
        {
            //GENERAL INFO
            detailedStockNameText.text = CurrentStock.name;
            detailedStockPriceText.text = CurrentStock.price.ToString("F2")+" HRN";
            stockIcon.sprite = Resources.Load<Sprite>("Icons/" + CurrentStock.name);
            
            var entry = Player.Instance.stocksInWallet.Find(s => s.stock == CurrentStock);
            if (entry != null)
            {
                detailedStockAMOUNTText.text ="Portfolio: "+ entry.amount.ToString() + " x " + CurrentStock.price.ToString("F2") + " HRN\n="+(entry.amount*CurrentStock.price).ToString("F2") + " HRN";  //$"{entry.amount} x {stock.price:F2} HRN";
                //portfolioValTotal.text = (entry.amount*stock.price).ToString("F2") + " HRN";
            }
            else
            {
                detailedStockAMOUNTText.text = "Portfolio: 0 x "+CurrentStock.price.ToString("F2") + " HRN\n= 0.00 HRN"; //$"{entry.amount} x {stock.price:F2} HRN";
            }
            
            int daysPassed = WeatherTime.Instance.daysPassed;
            int currentDay = daysPassed - 1;

            if (currentDay < 1 || CurrentStock.history.Count <= currentDay) return;

            // 1-Tages-Änderung
            float change1d = CurrentStock.GetPriceChangeRaw(1);
            float change1dPercent = CurrentStock.GetPriceChangePercent(1);
        
            if(change1d < 0) detailedStock24HText.text = "<color=#FF0000>\u25bc" + change1d.ToString("F2") +" HRN "+ change1dPercent.ToString("F2")+"%";
            if(change1d > 0) detailedStock24HText.text = "<color=#02fa02>\u25b2 +" + change1d.ToString("F2")+" HRN "+ change1dPercent.ToString("F2")+"%";
            if(change1d == 0)detailedStock24HText.text = "--- --- --- ---";
        

            // 7-Tages-Änderung oder seit Beginn
            int daysAgo = Mathf.Min(7, currentDay);
            float change7d = CurrentStock.GetPriceChangeRaw(daysAgo);
            float change7dPercent = CurrentStock.GetPriceChangePercent(daysAgo);
            if(change7d < 0) detailedStock7DText.text = "<color=#FF0000>\u25bc" + change7d.ToString("F2") +" HRN "+ change7dPercent.ToString("F2")+"%";
            if(change7d > 0) detailedStock7DText.text = "<color=#02fa02>\u25b2 +" + change7d.ToString("F2")+" HRN "+ change7dPercent.ToString("F2")+"%";
            
            // ALL Änderung
            float pastPrice = CurrentStock.history[0];
            float currentPrice = CurrentStock.history[currentDay];

            //return (currentPrice - pastPrice) / pastPrice * 100f;
            float changePrice = (currentPrice - pastPrice);
            float changePricePercent = (currentPrice - pastPrice) / pastPrice * 100f;
            if(changePrice < 0) detailedStockALLText.text = "<color=#FF0000>\u25bc" + changePrice.ToString("F2") +" HRN "+ changePricePercent.ToString("F2")+"%";
            if(changePrice > 0) detailedStockALLText.text = "<color=#02fa02>\u25b2 +" + changePrice.ToString("F2")+" HRN "+ changePricePercent.ToString("F2")+"%";
        }
        
        
    }

    

}
