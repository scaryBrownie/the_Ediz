using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPot : MonoBehaviour
{
    public int addHPoint, addMPoint;
    public void AddSomeHP()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        mc.currentHealth += addHPoint;
    }
    public void AddSomeMP()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        mc.currentMP += addMPoint;
    }
}
