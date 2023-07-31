using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class asd : MonoBehaviour
{
    public GameObject item;
    private Transform playerTransform;
    public float purchase;
    private TextMeshProUGUI purchaseText;

    bool useItemBool;

    public bool mcUsinNowArrow;

    public int addHPoint, addMPoint, addGRPoint;
    public void AddSomeHP(float addHP)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        mc.currentHealth += addHP;
        if (gameObject.name == "redPotionBig(Clone)" || gameObject.name == "redPotionorta(Clone)" || gameObject.name == "redPotionsmall(Clone)" || gameObject.name == "redPotionBig" || gameObject.name == "redPotionorta" || gameObject.name == "redPotionsmall")
        {
            mc.redParticle();
        }
    }
    public void AddSomeMP(float addMP)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        mc.currentMP += addMP;
        if (gameObject.name == "BluePotion1(Clone)" || gameObject.name == "Blue Potion 3 1(Clone)" || gameObject.name == "bluePotion2(Clone)")
        {
            mc.blueParticle();
        }
        else if (gameObject.CompareTag("yemek"))
        {
            mc.eatparticle.Play();
        }
    }
    public void AddSomeGR(float addGR)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        mc.currentHealth += addGR;
        mc.currentMP += addGR;
        if (gameObject.name == "GreenPotion1(Clone)" || gameObject.name == "GreenPotion3(Clone)" || gameObject.name == "GreenPotion2(Clone)")
        {
            mc.greenParticle();
        }
    }
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        purchaseText = GameObject.Find("HowMuchtoSell").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        RightClick();
    }
    public void SpawnItem()
    {
        purchaseText.text = "";
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.isOnPepper == true)
        {
            Pepper pp = GameObject.Find("Pepper").GetComponent<Pepper>();
            if (pp.isOnPepper)
            {
                if (pp.PepperMoney >= purchase)
                {
                    pp.PepperMoney -= purchase;
                    mc.money += purchase;
                    mc.coin.Play();
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(textCleaner());
                }
            }
        }
        else if (mc.isOnDealer == true)
        {
            WeaponDealer wp = GameObject.Find("WeaponDealer").GetComponent<WeaponDealer>();
            if (wp.isOnDealer)
            {
                if (wp.weaponDealerMoney >= purchase)
                {
                    wp.weaponDealerMoney -= purchase;
                    mc.money += purchase;
                    mc.coin.Play();
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(textCleaner());
                }
            }
        }
        else if (mc.isOnSaffron == true)
        {
            Saffron sf = GameObject.Find("Saffron").GetComponent<Saffron>();
            if (sf.isOnSaffron)
            {
                if (sf.SaffronMoney >= purchase)
                {
                    sf.SaffronMoney -= purchase;
                    mc.money += purchase;
                    mc.coin.Play();
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(textCleaner());
                }
            }
        }
        else
        {
            mc.throwItem.Play();

            if (gameObject.name == "ArrowNormal(mc)(Clone)")
            {
                mc.ArrowNormal.enabled = false;
            }
            else if (gameObject.name == "ArrowDark(mc)(Clone)")
            {
                mc.ArrowDark.enabled = false;
            }
            else if (gameObject.name == "ArrowGold(mc)(Clone)")
            {
                mc.ArrowGold.enabled = false;
            }
            else if (gameObject.name == "ArrowGreen(mc)(Clone)")
            {
                mc.ArrowGreen.enabled = false;
            }
                float x = UnityEngine.Random.Range(0.4f, 1.4f);
                Vector2 playerPos = new Vector2(playerTransform.position.x + x, playerTransform.position.y + x);
                Instantiate(item, playerPos, Quaternion.identity);
                Destroy(gameObject);
        }
    }

    public void onPointEnter()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.isOnDealer == true)
        {
            WeaponDealer wp = GameObject.Find("WeaponDealer").GetComponent<WeaponDealer>();
            if (wp.isOnDealer)
            {
                purchaseText.text = "Left click for sell. (" + purchase + "$)";
            }
        }
        else if (mc.isOnPepper == true)
        {
            Pepper pp = GameObject.Find("Pepper").GetComponent<Pepper>();
            if (pp.isOnPepper)
            {
                purchaseText.text = "Left click for sell. (" + purchase + "$)";
            }
        }
        else if (mc.isOnSaffron == true)
        {
            Saffron sf = GameObject.Find("Saffron").GetComponent<Saffron>();
            if (sf.isOnSaffron)
            {
                purchaseText.text = "Left click for sell. (" + purchase + "$)";
            }
        }
        else if (!mc.isOnDealer && !mc.isOnPepper && !mc.isOnSaffron && gameObject.tag == "potitem")
        {
            purchaseText.text = "Right click to use.";
            useItemBool = true;
        }
        else if (!mc.isOnDealer && !mc.isOnPepper && !mc.isOnSaffron && gameObject.tag == "arrow")
        {
            purchaseText.text = "Double right click to equip.";
            useItemBool = true;
        }
        else if (!mc.isOnDealer && !mc.isOnPepper && !mc.isOnSaffron && gameObject.tag == "yemek")
        {
            purchaseText.text = "Right click to eat.";
            useItemBool = true;
        }
    }   

    public void onPointExit()
    {
        useItemBool = false;
        purchaseText.text = "";
    }
    public IEnumerator textCleaner()
    {
        purchaseText.text = "The seller has no more money.";
        yield return new WaitForSeconds(1);
        purchaseText.text = "";
    }

    public void Zirh()
    {

    }
    public void RightClick()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (Input.GetMouseButtonDown(1) && useItemBool && gameObject.tag == "potitem" || Input.GetMouseButtonDown(1) && useItemBool && gameObject.tag == "yemek")
        {
            if (gameObject.tag == "potitem")
            {
                AddSomeHP(addHPoint);
                AddSomeMP(addMPoint);
                AddSomeGR(addGRPoint);
                mc.drinkPot.Play();
                Destroy(gameObject);
                purchaseText.text = "";
            }
            else if (gameObject.tag == "yemek")
            {
                AddSomeHP(addHPoint);
                AddSomeMP(addMPoint);
                AddSomeGR(addGRPoint);
                mc.eatMeal.Play();
                Destroy(gameObject);
                purchaseText.text = "";
            }
        }
        if (Input.GetMouseButtonDown(1) && useItemBool && gameObject.tag == "arrow")
        {
            mc.arrowGiy.Play();
            if (gameObject.name == "ArrowNormal(mc)(Clone)"  && mcUsinNowArrow || gameObject.name == "ArrowNormal(mc)" && mcUsinNowArrow)
            {
                mc.ArrowNormal.enabled = true;
                mc.ArrowDark.enabled = mc.ArrowGold.enabled = mc.ArrowGreen.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }
            else if (gameObject.name == "ArrowNormal(mc)(Clone)" && !mcUsinNowArrow || gameObject.name == "ArrowNormal(mc)" && !mcUsinNowArrow)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                mc.ArrowNormal.enabled = false;
                mc.ArrowDark.enabled = mc.ArrowGold.enabled = mc.ArrowGreen.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }



            if (gameObject.name == "ArrowGreen(mc)(Clone)" && mcUsinNowArrow || gameObject.name == "ArrowGreen(mc)" && mcUsinNowArrow)
            {
                mc.ArrowGreen.enabled = true;
                mc.ArrowDark.enabled = mc.ArrowGold.enabled = mc.ArrowNormal.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }
            else if (gameObject.name == "ArrowGreen(mc)(Clone)" && !mcUsinNowArrow || gameObject.name == "ArrowGreen(mc)" && !mcUsinNowArrow)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                mc.ArrowGreen.enabled = false;
                mc.ArrowDark.enabled = mc.ArrowGold.enabled = mc.ArrowNormal.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }



            if (gameObject.name == "ArrowGold(mc)(Clone)" && mcUsinNowArrow || gameObject.name == "ArrowGold(mc)" && mcUsinNowArrow)
            {
                mc.ArrowGold.enabled = true;
                mc.ArrowDark.enabled = mc.ArrowNormal.enabled = mc.ArrowGreen.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }
            else if (gameObject.name == "ArrowGold(mc)(Clone)" && !mcUsinNowArrow || gameObject.name == "ArrowGold(mc)" && !mcUsinNowArrow)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                mc.ArrowGold.enabled = false;
                mc.ArrowDark.enabled = mc.ArrowNormal.enabled = mc.ArrowGreen.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }




            if (gameObject.name == "ArrowDark(mc)(Clone)" && mcUsinNowArrow || gameObject.name == "ArrowDark(mc)" && mcUsinNowArrow)
            {
                mc.ArrowDark.enabled = true;
                mc.ArrowNormal.enabled = mc.ArrowGold.enabled = mc.ArrowGreen.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }
            else if (gameObject.name == "ArrowDark(mc)(Clone)" && !mcUsinNowArrow || gameObject.name == "ArrowDark(mc)" && !mcUsinNowArrow)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                mc.ArrowDark.enabled = false;
                mc.ArrowNormal.enabled = mc.ArrowGold.enabled = mc.ArrowGreen.enabled = false;
                mcUsinNowArrow = !mcUsinNowArrow;
            }
        }
    }
}
