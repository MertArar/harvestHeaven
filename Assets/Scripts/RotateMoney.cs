﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMoney : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;     
    void Update()
    {
        transform.Rotate(0f, 0f, turnSpeed * Time.fixedDeltaTime);
    }
}
