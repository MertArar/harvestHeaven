using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeController : MonoBehaviour
{
    [Space]
    [Header("Farmer Fruit Dotween")]
    private GameObject farmer;
    [SerializeField] private float duration;    
    [SerializeField] private float strength;    
    [SerializeField] private int vibrato;  
    [SerializeField] private float randomness; 
    [SerializeField] private float shakeTime = 0.5f;
    private Animator anim;
    private bool isWork;
    void Start()
    {
        this.farmer = this.transform.parent.gameObject.transform.GetComponent<FarmerManager>().gameObject;
        this.anim = this.farmer.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        StartCoroutine(nameof(ShakeTree));
    }
    void Update()
    {
        if (farmer.gameObject.GetComponent<FarmerManager>().isWorking)
        {
            
            this.anim.SetBool("isWorking", true);
        }
        else
        {
            this.anim.SetBool("isWorking", false);
        }
    }
    IEnumerator ShakeTree()
    {
        while (true)
        {
            if (farmer.gameObject.GetComponent<FarmerManager>().isWorking)
            {
                this.transform.DOShakeRotation(duration, strength, vibrato, randomness);
            }
            yield return new WaitForSeconds(shakeTime);
        }
    }
}
