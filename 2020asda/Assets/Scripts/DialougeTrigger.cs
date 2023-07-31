using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialouge;

    public void TriggerDialog()
    {
        if (gameObject.name == "Giris")
        {
            FindObjectOfType<DialougaMNGiris>().StartDialouge(dialouge);
        }
        else
        {
            FindObjectOfType<DialougeManager>().StartDialouge(dialouge);
        }     
    }
}
