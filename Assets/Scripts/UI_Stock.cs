using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Stock : MonoBehaviour
{
    [SerializeField] public string selectedStockKey;
    private Stock stock;

    [Header("Detailed Information")] 
    //public GameObject detailedViewGo;

    public TMP_Text stockName;
    public TMP_Text stockPrice;
    public Image stockIcon;
    
    [Header("History Elements")]
    public TMP_Text history1d;
    public TMP_Text history7d;

    [ReadOnly]
    public int dayNormed;
    
    [Header("Portfolio Elements")]
    public TMP_Text portfolioAmount;
    public TMP_Text portfolioValTotal;
    

    void Start()
    {
        stock = Stocks.GetStockByKey(selectedStockKey);
    }
    
    
    void Update()
    {
        if(UI_Handler.Instance == null) UI_Handler.Instance = GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<UI_Handler>();
        
        
        #region Edgecase for first day where there is no data yet
        if (WeatherTime.Instance.daysPassed <= 1)
        {
            float ch = (stock.price - stock.initialPrice);
            
            if(ch<0)history1d.text = "<color=#FF0000>\u25bc" +(stock.price-stock.initialPrice).ToString("F2") + " HRN";
            if(ch>0)history1d.text = "<color=#02fa02>\u25b2 " +(stock.price-stock.initialPrice).ToString("F2") + " HRN";
            
            if(ch<0)history7d.text = "<color=#FF0000>\u25bc" +(stock.price-stock.initialPrice).ToString("F2") + " HRN";
            if(ch>0)history7d.text = "<color=#02fa02>\u25b2 " +(stock.price-stock.initialPrice).ToString("F2") + " HRN";
            
            
            history7d.text = "NO DATA YET";
            
            
           
            
        }
        

        #endregion
        
        //IN PORTFOLIO
        var entry = Player.Instance.stocksInWallet.Find(s => s.stock == stock);
        if (entry != null)
        {
            portfolioAmount.text = entry.amount.ToString() + "x";  //$"{entry.amount} x {stock.price:F2} HRN";
            portfolioValTotal.text = (entry.amount*stock.price).ToString("F2") + " HRN";
        }
        else
        {
            portfolioAmount.text = "0x";
            portfolioValTotal.text = "0.00 HRN";
        }
        
        
        if (stock == null)
        {
            
            return;
        }

        stockName.text = stock.name;
        stockPrice.text = $"{stock.price:F2} HRN";
        stockIcon.sprite = Resources.Load<Sprite>("Icons/" + selectedStockKey);

        int daysPassed = WeatherTime.Instance.daysPassed;
        int currentDay = daysPassed - 1;

        if (currentDay < 1 || stock.history.Count <= currentDay) return;

        // 1-Tages-Änderung
        float change1d = stock.GetPriceChangeRaw(1);
        float change1dPercent = stock.GetPriceChangePercent(1);
        
        if(change1d < 0) history1d.text = "<color=#FF0000>\u25bc" + (change1d*(-1)).ToString("F2") +"HRN "+ (change1dPercent*(-1)).ToString("F2")+"%";
        if(change1d > 0) history1d.text = "<color=#02fa02>\u25b2 " + change1d.ToString("F2")+"HRN "+ change1dPercent.ToString("F2")+"%";
        if(change1d == 0)history1d.text = "--- --- --- ---";
        

        // 7-Tages-Änderung oder seit Beginn
        int daysAgo = Mathf.Min(7, currentDay);
        float change7d = stock.GetPriceChangeRaw(daysAgo);
        float change7dPercent = stock.GetPriceChangePercent(daysAgo);
        if(change7d < 0) history7d.text = "<color=#FF0000>\u25bc" + (change7d*(-1)).ToString("F2") +"HRN "+ (change7dPercent*(-1)).ToString("F2")+"%";
        if(change7d > 0) history7d.text = "<color=#02fa02>\u25b2 " + change7d.ToString("F2")+"HRN "+ change7dPercent.ToString("F2")+"%";
        //test
        
        
        
        
    }

    public void buyStock()
    {
        if (stock != null && Player.Instance.freeBalance >= stock.price)
        {
            Player.Instance.freeBalance -= stock.price;
            Player.Instance.addStock(stock);
            UI_Handler.Instance.createMouseNotification("1x "+stock.name+" bought!");
        }
    }

    public void sellStock()
    {
        var foundStock = Player.Instance.stocksInWallet.Find(s => s.stock == stock);
        if (foundStock != null && foundStock.amount > 0)
        {
            foundStock.amount--;
            Player.Instance.freeBalance += stock.price;
            if (foundStock.amount == 0)
            {
                Player.Instance.stocksInWallet.Remove(foundStock);
            }
                
           UI_Handler.Instance.createMouseNotification("1x "+stock.name+" sold!");
            
        }
    }

    public void showDetailedView()
    {
        if (!UI_Handler.Instance.detailedStockInfoWindow.activeSelf)
        {
            UI_Handler.Instance.detailedStockInfoWindow.SetActive(true);
        }
        else if(UI_Handler.Instance.detailedStockInfoWindow.activeSelf && UI_Handler.Instance.CurrentStock == stock)
        {
            UI_Handler.Instance.detailedStockInfoWindow.SetActive(false);
        }
        UI_Handler.Instance.CurrentStock = stock;
        EventSystem.current.SetSelectedGameObject(null);
    }
}

