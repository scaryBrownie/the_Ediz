using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class necromancerDeadScript : MonoBehaviour
{
    void Start()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.necromancerDead == true)
        {
            Destroy(gameObject);
        }
    }
}
