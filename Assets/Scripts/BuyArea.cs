using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private BuyAreaSO buyAreaType = null; 
    [Space]
    [Header("Shop Area Take")]
    [SerializeField] private GameObject farmerAndShopObject, buyObject;
    //[SerializeField] private int cost;   
    //[SerializeField] private int currentMoney; 
    [SerializeField] private float progress;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI areaText;
    public bool areaLocked;

    [Space]
    [Header("Shop Money Dotween")]
    [SerializeField] private float duration;    
    [SerializeField] private float strength;    
    [SerializeField] private int vibrato;  
    [SerializeField] private float randomness; 

    

    private void Start()
    {
        areaText.text = "$ " + (buyAreaType.cost - buyAreaType.currentMoney);
        this.areaLocked = buyAreaType.locked;
        if (!buyAreaType.locked)
        {
            
            this.buyObject.SetActive(false); 
            this.farmerAndShopObject.SetActive(true);    
            this.enabled = false;            
        }
    }
    public void Buy(int valueMoney)
    {
        if (buyAreaType.currentMoney < buyAreaType.cost)
        {
            buyAreaType.currentMoney += valueMoney; 
            areaText.text = "$ " + (buyAreaType.cost - buyAreaType.currentMoney);   
            progress = (buyAreaType.currentMoney / buyAreaType.cost);   
            progressImage.fillAmount = progress;    
            //SaveManager.savemanagerInstance.SaveGame(); 

            if (buyAreaType.currentMoney == buyAreaType.cost)
            {
                
                this.buyAreaType.locked = false;    
                this.areaLocked = buyAreaType.locked;   
                this.buyObject.SetActive(false);    
                this.farmerAndShopObject.SetActive(true);    
                this.farmerAndShopObject.transform.DOShakeScale(duration, strength, vibrato, randomness);   
                AudioController.audioControllerInstance.Play("BuyAreaSound"); 
                //SaveManager.savemanagerInstance.SaveGame();
                this.enabled = false; 
            }
        }
    }
}
