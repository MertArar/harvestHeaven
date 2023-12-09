using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); 
    [SerializeField] private Transform collectPoint;  
    [SerializeField] private float fruitBetween = 10f;  
    [SerializeField] private int fruitCollectLimit = 10; 
    [Space]
    [Header("Object Pool")]
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;

    private void OnEnable()
    {
        TriggerEventManager.OnFruitCollet += GetFruit; 
        TriggerEventManager.OnFruitGive += GiveShopFruit; 
    }
    private void OnDisable()
    {
        TriggerEventManager.OnFruitCollet -= GetFruit;  
        TriggerEventManager.OnFruitGive -= GiveShopFruit;  

    }
    private void GetFruit()
    {
       
        if (fruitList.Count < fruitCollectLimit)
        {
            poolValue = TriggerEventManager.farmerManager.publicpoolValue+4; 
           
            GameObject newCollectFruit = objectPool.GetPooledObject(poolValue);    
            newCollectFruit.transform.parent = collectPoint;   
            newCollectFruit.transform.position = new Vector3(collectPoint.position.x,
                ((float)fruitList.Count / fruitBetween) + collectPoint.position.y,
                collectPoint.position.z);      
            fruitList.Add(newCollectFruit); 
            AudioController.audioControllerInstance.Play("FruitSound"); 
            if (TriggerEventManager.farmerManager != null)
            {
                TriggerEventManager.farmerManager.RemoveLastFruit();
            }
        }
    }
    public void RemoveLastFruit()
    {
        
        if (fruitList.Count > 0)
        {
            objectPool.SetPooledObject(fruitList[fruitList.Count - 1], poolValue);  
            fruitList[fruitList.Count - 1].transform.parent = GameObject.Find("FruitObjects").gameObject.transform; 
            fruitList.RemoveAt(fruitList.Count - 1); 
        }
    }
    public void GiveShopFruit()
    {
       
        if (fruitList.Count > 0)
        {
           
            if (TriggerEventManager.shopManager.fruitList.Count < TriggerEventManager.shopManager.maxFruit)
            {
                TriggerEventManager.shopManager.GetFruit();
                RemoveLastFruit(); 
            }
            
        }
    }
}
