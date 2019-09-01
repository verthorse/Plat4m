﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotateController : MonoBehaviour
{
    public float degreesPerSecond = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0); 
    }
}
