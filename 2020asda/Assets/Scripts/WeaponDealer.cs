using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponDealer : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    public Image BGimage;

    public GameObject Tezgah;

    public bool isOnDealer;
    bool inventoryToggle;

    public float weaponDealerMoney;
    public TextMeshProUGUI weaponDealerMoneytext;

    void Start()
    {
        BGimage.enabled = false;
        textMesh.text = "";
        Tezgah.SetActive(false);
    }

    void Update()
    {
        weaponDealerMoneytext.text = weaponDealerMoney + "$";
        Tezgaah();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.gameObject.tag == "Player")
        {
            mc.isOnDealer = true;
            inventoryToggle = true;
            isOnDealer = true;
            BGimage.enabled = true;
            textMesh.text = "Press 'E'.";
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        
        if (collision.gameObject.tag == "Player")
        {
            mc.isOnDealer = false;
            isOnDealer = false;
            textMesh.text = "";
            BGimage.enabled = false;
        }
    }
    void Tezgaah()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.isOnDealer)
        {
            if (!mc.goWP)
            {
                if (Input.GetKeyDown(KeyCode.E) && inventoryToggle && isOnDealer)
                {
                    mc.click.Play();

                    Tezgah.SetActive(true);
                    mc.inventory.SetActive(true);
                    mc.inventoryToggle = !mc.inventoryToggle;
                    inventoryToggle = !inventoryToggle;
                }
                else if (Input.GetKeyDown(KeyCode.E) && !inventoryToggle || !isOnDealer)
                {
                    mc.click.Play();

                    Tezgah.SetActive(false);
                    mc.inventory.SetActive(false);
                    mc.inventoryToggle = !mc.inventoryToggle;
                    inventoryToggle = !inventoryToggle;
                }
            }
            else if (mc.yedincigorev)
            {
                if (Input.GetKeyDown(KeyCode.E) && isOnDealer)
                {
                    mc.click.Play();

                    mc.hiz = 0;
                    GameObject.Find("7MS").GetComponent<DialougeTrigger>().TriggerDialog();
                }
            }
        }
        else
        {
            Tezgah.SetActive(false);
        }
    }
}
