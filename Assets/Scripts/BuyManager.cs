using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{    
    [SerializeField] private int totalMoney=0;
    public int publicTotalMoney
    {
        get { return totalMoney; }
        set { totalMoney = value; }
    }

    private void OnEnable()
    {
        TriggerEventManager.OnMoneyCollected += IncreaseMoney;
        TriggerEventManager.OnBuyShopAndFarmer += BuyArea;
    }
    private void OnDisable()
    {
        TriggerEventManager.OnMoneyCollected -= IncreaseMoney;
        TriggerEventManager.OnBuyShopAndFarmer -= BuyArea;
    }
    void BuyArea()
    {
        if (TriggerEventManager.buyArea != null)
        {
            if (totalMoney >= 1 && TriggerEventManager.buyArea.areaLocked)
            {
                TriggerEventManager.buyArea.Buy(1);
                totalMoney -= 1;
            }
        }
    }
    private void IncreaseMoney()
    {
        if (TriggerEventManager.shopManager.moneyList.Count > 0)
        {
            totalMoney += 5;   
            TriggerEventManager.shopManager.RemoveLastMoney();  
            AudioController.audioControllerInstance.Play("MoneySound");
        }        
    }
}
