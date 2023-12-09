using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); 
    public List<GameObject> moneyList = new List<GameObject>(); 
    [Space]
    [Header("Shop Fruit Genarete")]
    [SerializeField] private float fruitBetween=6;
    [SerializeField] private int stackCount = 10;   
    public int maxFruit = 150;   
    [SerializeField] private Transform givePoint;  
    [SerializeField] private GameObject fullFruit;
    bool isWorking;
    [Space]
    [Header("Shop Money Genarete")]
    [SerializeField] private GameObject moneyPrefab;   
    [SerializeField] private GameObject rotateMoney;   
    [SerializeField] private float moneySpawnerTime = 0.8f;  
    [SerializeField] private float moneyBetween = 4f;  
    [SerializeField] private int moneyStackCountX = 5;   
    [SerializeField] private int moneyStackCountY = 25;   
    [SerializeField] private int maxMoney = 100;   
    [SerializeField] private Transform moneySpawnPoint; 
    bool isMoneySpawner;
    [Space]
    [Header("Object Pool")]
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;
    [Space]
    [Header("Shop Money Dotween")]
    [SerializeField] private float duration;    
    [SerializeField] private float strength;   
    [SerializeField] private int vibrato;  
    [SerializeField] private float randomness; 

    private void Start()
    {
        StartCoroutine(nameof(GenareteMoney));
    }
    
    IEnumerator GenareteMoney()
    {        
        while (true)
        {
            if (moneyList.Count < maxMoney)
            {
                isMoneySpawner = true;
            }
            else
            {
                isMoneySpawner = false;
            }
            if (fruitList.Count > 0 && isMoneySpawner)
            {
                
                float moneyCount = moneyList.Count;
                int colCount = (int)moneyCount / moneyStackCountX;    
                int rowCount = (int)moneyCount / moneyStackCountY;   
              
                    GameObject newMoney = objectPool.GetPooledObject(0);   
                    newMoney.transform.position = new Vector3(moneySpawnPoint.position.x + ((moneyCount % moneyStackCountX) / moneyBetween),
                                moneySpawnPoint.position.y + ((float)rowCount / 10) + 0.05f,
                                moneySpawnPoint.position.z + ((float)colCount / 2) - ((2 * rowCount) + ((float)rowCount / 2f)));
                    newMoney.transform.DOShakeScale(duration, strength, vibrato, randomness);   
                    moneyList.Add(newMoney);                
                    RemoveLastFruit();
            }
            yield return new WaitForSeconds(moneySpawnerTime);
        }        
    }
    private void Update()
    {
        if (moneyList.Count > 0)
        {
            
            rotateMoney.SetActive(true);
        }
        else
        {
            rotateMoney.SetActive(false);
        }
        if (fruitList.Count >= maxFruit)
        {
            fullFruit.SetActive(true);
        }
        else
        {
            fullFruit.SetActive(false);
        }
    }
    public void GetFruit()
    {
       
        float fruitCount = fruitList.Count;
        int colCount = (int)fruitCount / stackCount;   
        if (isWorking)
        {            
            
            FruitSelective();  
            GameObject newGiveFruit = objectPool.GetPooledObject(poolValue);   
            newGiveFruit.transform.position = new Vector3(givePoint.position.x + ((fruitCount % stackCount) / fruitBetween),
                        givePoint.position.y + 0.1f,
                        givePoint.position.z + ((float)colCount / 6));
            fruitList.Add(newGiveFruit); 
            AudioController.audioControllerInstance.Play("FruitSound"); 
            if (fruitList.Count >= maxFruit)
            {
                isWorking = false;                 
            }
        }
        else if (fruitList.Count < maxFruit)
        {
            isWorking = true;                  
        }
    }
    public void RemoveLastFruit()
    {
        
        if (fruitList.Count > 0)
        {
          
            objectPool.SetPooledObject(fruitList[fruitList.Count - 1], poolValue);  
             
            fruitList.RemoveAt(fruitList.Count - 1);  
        }
    }
    public void RemoveLastMoney()
    {
       
        if (moneyList.Count > 0)
        {
           
            objectPool.SetPooledObject(moneyList[moneyList.Count - 1], 0); 
           
            moneyList.RemoveAt(moneyList.Count - 1);  
        }
    }
    void FruitSelective()
    {
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Apple")
        {
            poolValue = 9;
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Banana")
        {
            poolValue = 10;
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Carrot")
        {
            poolValue = 11;
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Mushroom")
        {
            poolValue = 12;
        }
    }
}
