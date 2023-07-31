using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DealItemSaffron : MonoBehaviour
{
    public GameObject ittemButton;
    public int purchase;
    private TextMeshProUGUI purchaseText;

    void Start()
    {
        
    }
    public void ItemPazar()
    {
        purchaseText = GameObject.Find("HowMuchtoSell").GetComponent<TextMeshProUGUI>();
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        inventory inv = GameObject.FindWithTag("Player").GetComponent<inventory>();
        
        for (int i = 0; i < inv.inventorySlots.Length; i++)
        {
            if (inv.isFull[i] == false)
            {
                if (mc.money >= purchase)
                {
                    mc.coin.Play();

                    mc.money -= purchase;
                    if (mc.isOnPepper)
                    {
                        Pepper pp = GameObject.Find("Pepper").GetComponent<Pepper>();
                        pp.PepperMoney += purchase;
                    }
                    else if (mc.isOnSaffron)
                    {
                        Saffron sf = GameObject.Find("Saffron").GetComponent<Saffron>();
                        sf.SaffronMoney += purchase;
                        
                    }
                    else if (mc.isOnDealer)
                    {
                        WeaponDealer wp = GameObject.Find("WeaponDealer").GetComponent<WeaponDealer>();
                        wp.weaponDealerMoney += purchase;
                    }

                    purchaseText.text = "";
                    inv.isFull[i] = true;
                    Instantiate(ittemButton, inv.inventorySlots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
                else
                {
                    StartCoroutine(AzWait());
                }
            }
        }
    }

    public void onMouseEnter()
    {
        purchaseText = GameObject.Find("HowMuchtoSell").GetComponent<TextMeshProUGUI>();
        purchaseText.text = "Left click for buy.("+purchase+"$)";
    }
    public void onMouseExit()
    {
        purchaseText = GameObject.Find("HowMuchtoSell").GetComponent<TextMeshProUGUI>();
        purchaseText.text = "";
    }
    IEnumerator AzWait()
    {
        purchaseText = GameObject.Find("HowMuchtoSell").GetComponent<TextMeshProUGUI>();
        purchaseText.text = "Yo need more money.";
        yield return new WaitForSeconds(1.3f);
        purchaseText.text = "";
    }
}
