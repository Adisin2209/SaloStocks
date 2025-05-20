using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [Header("Game Settings")] 
    public bool AutoSave = false;
    
    public float freeBalance;
    public float portfolioBalance;

    [System.Serializable]
    public class stockInWallet
    {
        public Stock stock;
        public int amount;

        public stockInWallet(Stock stock, int amount)
        {
            this.stock = stock;
            this.amount = amount;
        }
        
    }
    
    public List<stockInWallet> stocksInWallet = new();

    public void addStock(Stock stock)
    {
            foreach (stockInWallet s in stocksInWallet)
            {
                if (s.stock == stock)
                {
                    s.amount++;
                    return;
                }
            } 
            stocksInWallet.Add(new stockInWallet(stock, 1));
    }
    // Start is called before the first frame update

    public void calcAmount()
    {
        float value = 0;
        foreach (var s in stocksInWallet)
        {
            value += s.stock.price * s.amount;
        }
        portfolioBalance = value;
    }
    
    void Start()
    {
        Instance = this;
    }
    
    
}
