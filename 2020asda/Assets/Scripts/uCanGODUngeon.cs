using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uCanGODUngeon : MonoBehaviour
{

    void LateUpdate()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (!mc.goBackkDungeon)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
