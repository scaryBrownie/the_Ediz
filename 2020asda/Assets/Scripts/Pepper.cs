using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pepper : MonoBehaviour
{

    public TextMeshProUGUI PressText;
    public Image PressImage;
    public GameObject Tezgah;
    public bool isOnPepper;
    bool inventoryToggle;
    public float PepperMoney;
    public TextMeshProUGUI pepperMoneytxt;

    void Start()
    {
        isOnPepper = false;
        PressImage.enabled = false;
        PressText.enabled = false;
        Tezgah.SetActive(false);
        inventoryToggle = true;
    }

    void Update()
    {
        Tezgaah();
        pepperMoneytxt.text = PepperMoney + "$";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.CompareTag("Player"))
        {
            isOnPepper = true;
            PressImage.enabled = true;
            PressText.enabled = true;
            mc.isOnPepper = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.CompareTag("Player"))
        {
            isOnPepper = false;
            PressImage.enabled = false;
            PressText.enabled = false;
            mc.isOnPepper = false;
        }
    }

    void Tezgaah()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.isOnPepper)
        {
            if (!mc.goPepper)
            {
                if (Input.GetKeyDown(KeyCode.E) && inventoryToggle && isOnPepper)
                {
                    mc.click.Play();

                    Tezgah.SetActive(true);
                    mc.inventory.SetActive(true);
                    mc.inventoryToggle = !mc.inventoryToggle;
                    inventoryToggle = !inventoryToggle;
                }
                else if (Input.GetKeyDown(KeyCode.E) && !inventoryToggle || !isOnPepper)
                {
                    mc.click.Play();

                    Tezgah.SetActive(false);
                    mc.inventory.SetActive(false);
                    mc.inventoryToggle = !mc.inventoryToggle;
                    inventoryToggle = !inventoryToggle;
                }
            }


            else if (mc.besinciGorev && Input.GetKeyDown(KeyCode.E) && isOnPepper)
            {
                mc.click.Play();

                mc.hiz = 0;
                GameObject.Find("5.gorev").GetComponent<DialougeTrigger>().TriggerDialog();
            }
            else if (mc.altincigorev && Input.GetKeyDown(KeyCode.E) && isOnPepper)
            {
                mc.click.Play();

                mc.hiz = 0;
                GameObject.Find("6MS").GetComponent<DialougeTrigger>().TriggerDialog();
                Vector2 oldinv = mc.inventory.transform.position;
                Vector2 inv = new Vector2(mc.inventory.transform.position.x + 50, mc.inventory.transform.position.y);
                mc.inventory.transform.position = inv;

                mc.inventory.SetActive(true);
                while (true)
                {
                    try
                    {
                        Destroy(GameObject.FindWithTag("dal"));
                    }
                    catch (Exception)
                    {
                        break;
                    }
                    break;
                }
                

                mc.inventory.transform.position = oldinv;
                mc.inventory.SetActive(false);
            }
        }

        else
        {
            Tezgah.SetActive(false);
        }
    }
}
