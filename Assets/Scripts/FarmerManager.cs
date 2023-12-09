using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarmerManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>();    
    [Space]
    [Header("Fruit Spawner")]
    [SerializeField] private float spawnerTime = 0.5f;  
    [SerializeField] private float fruitBetween = 10f;  
    [SerializeField] private int maxFruit = 50; 
    [SerializeField] private int stackCount = 10;  
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private GameObject fullFruit;
    public bool isWorking;
    [Space]
    [Header("Object Pool")]
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;
    public int publicpoolValue
    {
        get { return poolValue; }
        set { poolValue = value; }
    }

    [Space]
    [Header("Fruit Jump Dotween")]
    [SerializeField] private GameObject jumpFruitObject;
    [SerializeField] private Vector3 jumpFruitObjectStartPositoin;
    [SerializeField] private float duration;   
    void Start()
    {
        StartCoroutine(nameof(FarmerFruitSpawner));
    }    
    void Update()
    {
        
    }
    IEnumerator FarmerFruitSpawner()
    {
        while (true)
        {
            float fruitCount = fruitList.Count;
            int colCount = (int)fruitCount / stackCount;    

           
            if (isWorking)  
            {
                GameObject newFruit = objectPool.GetPooledObject(poolValue);   
                newFruit.transform.position = new Vector3(spawnPoint.position.x + ((fruitCount % stackCount) / fruitBetween),
                    spawnPoint.position.y + 0.1f,
                    spawnPoint.position.z + ((float)colCount / 3));
                fruitList.Add(newFruit);            
                jumpFruitObject.transform.DOMove(new Vector3(newFruit.transform.position.x, 0f, newFruit.transform.position.z), duration);
                jumpFruitObject.transform.localPosition = jumpFruitObjectStartPositoin;

                if (fruitList.Count >= maxFruit)
                {
                    isWorking = false;
                    fullFruit.SetActive(true);
                }
            }
            else if (fruitList.Count < maxFruit)
            {
                isWorking = true;  
                fullFruit.SetActive(false);
            }

            yield return new WaitForSeconds(spawnerTime);   
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
}
