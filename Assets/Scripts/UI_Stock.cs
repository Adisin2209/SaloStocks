using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Stock : MonoBehaviour
{
    [SerializeField] public string selectedStockKey;
    private Stock stock;

    public TMP_Text stockName;
    public TMP_Text stockPrice;
    public Image stockIcon;
    
    [Header("History Elements")]
    public TMP_Text history1d;
    public TMP_Text history7d;
    public TMP_Text historyAll;
    public TMP_Text historySinceBought;
    [ReadOnly]
    public int dayNormed;
    

    void Start()
    {
        stock = Stocks.GetStockByKey(selectedStockKey);
    }
    
    
    void Update()
    {
        if (stock == null) return;

        stockName.text = stock.name;
        stockPrice.text = $"{stock.price:F2} HRN";
        stockIcon.sprite = Resources.Load<Sprite>("Icons/" + selectedStockKey);

        int daysPassed = WeatherTime.Instance.daysPassed;
        int currentDay = daysPassed - 1;

        if (currentDay < 1 || stock.history.Count <= currentDay) return;

        // 1-Tages-Änderung
        float change1d = stock.GetPriceChangeRaw(1);
        float change1dPercent = stock.GetPriceChangePercent(1);
        
        if(change1d < 0) history1d.text = "24H: <color=#FF0000>" + change1d.ToString("F2") +"HRN ["+ change1dPercent.ToString("F2")+"]%";
        if(change1d > 0) history1d.text = "24H: <color=#02fa02>+" + change1d.ToString("F2")+"HRN ["+ change1dPercent.ToString("F2")+"%]";
        if(change1d == 0)history1d.text = "24H: --- --- --- ---";
        

        // 7-Tages-Änderung oder seit Beginn
        int daysAgo = Mathf.Min(7, currentDay);
        float change7d = stock.GetPriceChangeRaw(daysAgo);
        float change7dPercent = stock.GetPriceChangePercent(daysAgo);
        if(change7d < 0) history7d.text = "7D: <color=#FF0000>" + change7d.ToString("F2") +"HRN ["+ change7dPercent.ToString("F2")+"]%";
        if(change7d > 0) history7d.text = "7D: <color=#02fa02>+" + change7d.ToString("F2")+"HRN ["+ change7dPercent.ToString("F2")+"%]";
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
}

