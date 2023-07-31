using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFullControl : MonoBehaviour
{
    public inventory inventory;
    public int i;
    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<inventory>();   
    }
    void Update()
    {
        if (transform.childCount <= 1)
        {
            inventory.isFull[i] = false;
        }
    }
}
