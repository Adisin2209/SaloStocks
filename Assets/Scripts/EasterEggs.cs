using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EasterEggs : MonoBehaviour
{
    [ReadOnly] public string loggedKeys;
    
    [ReadOnly] public List<Event> events;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        events = Events.eventList;
        
        KeyLogger();
        triggerEasterEgg();
    }

    void resetInputs()
    {
        loggedKeys = "";
    }
    
    void KeyLogger()
    {
        foreach (var c in Input.inputString)
        {
            loggedKeys += c;
        }
    }

    void triggerEasterEgg()
    {
        switch (true)
        {
            //HARTZ IV HATE ECONOMY
            case bool _ when loggedKeys.Contains("hartz4"):
                Debug.Log("HartIV EasterEgg Triggered");
                Stocks.AddStock("Martin Strays", 100f);
                Stocks.AddStock("Andrii Strays", 100f);
                Stocks.AddStock("Mustafa Strays", 100f);
                Stocks.AddStock("Hadid Strays", 100f);
                
                #region Martin
                Events.addEvent(
                    
                    "Martin throwed Hadid under the bus",
                    "Beim Versuch Guzz (Goettingen Huzz) zu impressen, zeigt Martin alte verwerfliche Bilder von Hadid, wofuer er aber in Hartz IV geslandert wird..",
                    100,
                    new List<StockImpact>
                    {
                        new StockImpact(Stocks.GetStockByKey("HadidStrays"), -15f),
                        new StockImpact(Stocks.GetStockByKey("MartinStrays"), 15f),
                        new StockImpact(Stocks.GetStockByKey("AndriiStrays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("MustafaStrays"), -5f),
                    }
                );
                
                Events.addEvent(
                    
                    "Martin rennt vor weisser Frau Weg",
                    "Als Martin eine gewisse weisse Frau erblickte, schien er sich auf dem Fahrrand umzudrehen und einfach weg zu fahren.",
                    100,
                    new List<StockImpact>
                    {
                        new StockImpact(Stocks.GetStockByKey("HadidStrays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("MartinStrays"), 15f),
                        new StockImpact(Stocks.GetStockByKey("AndriiStrays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("MustafaStrays"), -5f),
                    }
                );
                
                

                #endregion
                
                #region Andrii
                
                Events.addEvent(
                    "Andrii schickt screenshot von Sparkasse",
                    "Andrii schickt bereits in der mitte des Monats ein Screenshot seiner Sparkasse App. Er braucht 80 Euro um trotzdem kein Geld zu haben.",
                    100,
                    new List<StockImpact>
                    {
                        new StockImpact(Stocks.GetStockByKey("Hadid Strays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("Martin Strays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("Andrii Strays"), 15f),
                        new StockImpact(Stocks.GetStockByKey("Mustafa Strays"), -5f),
                    }
                    
                );
                #endregion
                
                #region Mustafa
                
                Events.addEvent(
                    "Mustafa stellt Sexfrage im Dominos-Team.",
                    "Mustafa fragt ein einem random Tag seine Mitarbeiterin und Schichtleiterin, ob sie und ihr freund bereits Sex hatten. Nach diesem Vorffall beatet er nie weider die gay Bestfriend allegations.",
                    100,
                    new List<StockImpact>
                    {
                        new StockImpact(Stocks.GetStockByKey("Hadid Strays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("Martin Strays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("Andrii Strays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("Mustafa Strays"), 15f),
                    }
                    
                );
                #endregion

                #region Hadid
                
                Events.addEvent(
                    "Eine von Hadids Ex-Quest gluecklich vergeben?",
                    "'It should have been me!’ sagte er noch zum hundertsten mal, nachdem Mustafa ein Bild von einer ehemaligen Flamme Hadids schickt, aus der nie was geworden ist.",
                    100,
                    new List<StockImpact>
                    {
                        new StockImpact(Stocks.GetStockByKey("Hadid Strays"), 15f),
                        new StockImpact(Stocks.GetStockByKey("Martin Strays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("Andrii Strays"), -5f),
                        new StockImpact(Stocks.GetStockByKey("Mustafa Strays"), -5f),
                    }
                    
                );
                

                #endregion
                
                
                resetInputs();
                break;
            
                
            
            
            
            default: break;
        }
        
    }
}
