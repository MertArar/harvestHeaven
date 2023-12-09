using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {  
        public Queue<GameObject> pooledObjects; 
        public GameObject objectPrefab;  
        public int poolSize;  
    }

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {        
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>(); 
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject newObj = Instantiate(pools[j].objectPrefab);  
                newObj.SetActive(false);    
                newObj.transform.parent = GameObject.Find("FruitObjects").gameObject.transform;   
                newObj.name = pools[j].objectPrefab.gameObject.name;
                pools[j].pooledObjects.Enqueue(newObj);  
            }
        }
    }
    public GameObject GetPooledObject(int objectType)
    {
        
        if (objectType >= pools.Length) return null;
        GameObject newObj = pools[objectType].pooledObjects.Dequeue();   
        newObj.SetActive(true);
        return newObj;
    }
    public void SetPooledObject(GameObject poolObject, int objectType)
    {
      
        if (objectType >= pools.Length) return;
        pools[objectType].pooledObjects.Enqueue(poolObject);  
        poolObject.SetActive(false);
    }
}