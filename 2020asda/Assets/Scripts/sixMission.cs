using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sixMission : MonoBehaviour
{
    public GameObject[] agacs;

    public bool missionOver6;

    public GameObject door1, door2, door3;

    void Start()
    {
        missionOver6 = false;
    }

    void Update()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.birdn)
        {
            door1.SetActive(true);
            door2.SetActive(false);
            door3.SetActive(false);
        }
        else if (mc.ikidn)
        {
            door1.SetActive(false);
            door2.SetActive(true);
            door3.SetActive(false);
        }
        else if (mc.ucdn)
        {
            door1.SetActive(false);
            door2.SetActive(false);
            door3.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.gameObject.tag == "Player" && mc.altincigorev && !missionOver6)
        {
            foreach (GameObject i in agacs)
            {
                i.SetActive(true);
            }
        }
    }
}
