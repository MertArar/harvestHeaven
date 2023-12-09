using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    public delegate void OnFruitCollectArea();  
    public static event OnFruitCollectArea OnFruitCollet;   
    public delegate void OnFruitGiveArea();  
    public static event OnFruitGiveArea OnFruitGive;   
    public delegate void OnMoneyArea(); 
    public static event OnMoneyArea OnMoneyCollected;  
    public delegate void OnBuyArea();  
    public static event OnBuyArea OnBuyShopAndFarmer; 

    public static FarmerManager farmerManager;  
    public static ShopManager shopManager;  
    public static CollectManager collectManager;  
    public static BuyArea buyArea;

    [SerializeField] private float fruitCollectTime = 0.5f; 
    bool isCollecting,isGiving,isTakeMoney;  
    void Start()
    {
        StartCoroutine(nameof(CollectEnum)); 
    }  
    IEnumerator CollectEnum()
    {
       
        while (true)
        {
            if (isCollecting)
            {
                OnFruitCollet();                   
            }
            if (isGiving)
            {
                
                OnFruitGive();    
            }
            if (isTakeMoney)
            {
               
                OnMoneyCollected();    
            }
            yield return new WaitForSeconds(fruitCollectTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("FruitCollectArea"))
        {
           
            isCollecting = true;
           
            farmerManager = other.gameObject.GetComponent<FarmerManager>();            
        }
        if (other.gameObject.CompareTag("FruitGiveArea"))
        {
           
            isGiving = true;
           
            shopManager = other.gameObject.GetComponent<ShopManager>();
            
            collectManager = gameObject.GetComponent<CollectManager>();
        }
        if (other.gameObject.CompareTag("MoneyArea"))
        {
           
            isTakeMoney = true;
           
            shopManager = other.transform.parent.gameObject.GetComponent<ShopManager>();
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {
            OnBuyShopAndFarmer();
           
            buyArea = other.gameObject.GetComponent<BuyArea>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FruitCollectArea"))
        {
            
            isCollecting = false;
            farmerManager = null;   
        }
        if (other.gameObject.CompareTag("FruitGiveArea"))
        {            
            isGiving = false;
            shopManager = null;  
            collectManager = null;  
        }
        if (other.gameObject.CompareTag("MoneyArea"))
        {
            
            isTakeMoney = false;  
            shopManager = null; 
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {            
            buyArea = null; 
        }
    }
}
