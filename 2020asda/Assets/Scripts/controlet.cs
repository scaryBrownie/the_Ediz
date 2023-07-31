using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlet : MonoBehaviour
{
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<mcScript>().isOnPepper = false;
    }
}
