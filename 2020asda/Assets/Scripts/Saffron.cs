using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using UnityEngine.Playables;
using System;

public class Saffron : MonoBehaviour
{

    public TextMeshProUGUI PressText;
    public UnityEngine.UI.Image PressImage;
    public GameObject Tezgah, atesTopu;
    public bool isOnSaffron;
    bool inventoryToggle;
    public float SaffronMoney;
    public TextMeshProUGUI SaffronMoneytxt;
    public Animator anim;
    GameObject mc;
    public Color color;
    public bool assa, yukari, sag, sol, stopp, textControl;
    public Sprite spUp;

    public ParticleSystem atesParticle;

    void Start()
    {
        assa = yukari = sag = sol = stopp = false;
        isOnSaffron = false;
        PressImage.enabled = false;
        PressText.enabled = false;
        Tezgah.SetActive(false);
        inventoryToggle = true;
        anim = gameObject.GetComponent<Animator>();

        color = PressImage.color;
    }

    void Update()
    {
        
        if (!textControl)
        {
            Tezgaah();
            SaffronMoneytxt.text = SaffronMoney + "$";
        }
        

        if (sag)
        {
            anim = gameObject.GetComponent<Animator>();
            anim.SetBool("isMoving", true);
            anim.SetFloat("Horizontal", 1);
            anim.SetFloat("Vertical", 0);
        }
        else if (sol)
        {
            anim = gameObject.GetComponent<Animator>();
            anim.SetBool("isMoving", true);
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Vertical", 0);
        }
        else if (assa)
        {
            anim = gameObject.GetComponent<Animator>();
            anim.SetBool("isMoving", true);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", -1);
        }
        else if (yukari)
        {
            anim = gameObject.GetComponent<Animator>();
            anim.SetBool("isMoving", true);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 1);
        }
        else
        {
            anim = gameObject.GetComponent<Animator>();
            anim.SetBool("isMoving", false);
            mc = GameObject.FindWithTag("Player");
            anim.SetFloat("Horizontal", mc.transform.position.x -gameObject.transform.position.x);
            anim.SetFloat("Vertical", mc.transform.position.y - gameObject.transform.position.y);
        }
        if (stopp)
        {
            try
            {
                GameObject.Find("Saf1").GetComponent<DialougeTrigger>().TriggerDialog();
            }
            catch (Exception)
            {
            }
            try
            {
                GameObject.Find("Saffron1").GetComponent<DialougeTrigger>().TriggerDialog();
            }
            catch (Exception)
            {
            }
            stopp = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.CompareTag("Player"))
        {
            isOnSaffron = true;
            PressImage.enabled = true;
            PressText.enabled = true;
            mc.isOnSaffron  = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.CompareTag("Player"))
        {
            isOnSaffron = false;
            PressImage.enabled = false;
            PressText.enabled = false;
            mc.isOnSaffron = false;
        }
    }

    void Tezgaah()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.isOnSaffron)
        {
            if (!mc.goSaffron)
            {
                if (Input.GetKeyDown(KeyCode.E) && inventoryToggle && isOnSaffron)
                {
                    mc.click.Play();

                    Tezgah.SetActive(true);
                    mc.inventory.SetActive(true);
                    mc.inventoryToggle = !mc.inventoryToggle;
                    inventoryToggle = !inventoryToggle;
                }
                else if (Input.GetKeyDown(KeyCode.E) && !inventoryToggle || !isOnSaffron)
                {
                    mc.click.Play();

                    Tezgah.SetActive(false);
                    mc.inventory.SetActive(false);
                    mc.inventoryToggle = !mc.inventoryToggle;
                    inventoryToggle = !inventoryToggle;
                }
            }
            else if (mc.ikinciGorev)
            {
                if (Input.GetKeyDown(KeyCode.E) && isOnSaffron)
                {
                    mc.click.Play();

                    mc.hiz = 0;
                    GameObject.Find("Saf2").GetComponent<DialougeTrigger>().TriggerDialog();
                }
            }
            else if (mc.dorduncuGorev)
            {
                if (Input.GetKeyDown(KeyCode.E) && isOnSaffron)
                {
                    mc.click.Play();

                    mc.hiz = 0;
                    GameObject.Find("saf4").GetComponent<DialougeTrigger>().TriggerDialog();
                    Vector2 oldinv = mc.inventory.transform.position;
                    Vector2 inv = new Vector2(mc.inventory.transform.position.x +50, mc.inventory.transform.position.y);
                    mc.inventory.transform.position = inv;

                    mc.inventory.SetActive(true);
                    while (true)
                    {
                        try
                        {
                            Destroy(GameObject.FindWithTag("item"));
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
            else if (mc.sekizincigorev)
            {
                if (Input.GetKeyDown(KeyCode.E) && isOnSaffron)
                {
                    mc.click.Play();

                    mc.hiz = 0;
                    GameObject.Find("yedinciGorev").GetComponent<DialougeTrigger>().TriggerDialog();
                }
            }
            else if (mc.onbirincigorev)
            {
                if (Input.GetKeyDown(KeyCode.E) && isOnSaffron)
                {
                    mc.click.Play();

                    mc.hiz = 0;
                    GameObject.Find("dokuzSf").GetComponent<DialougeTrigger>().TriggerDialog();

                    Vector2 oldinv = mc.inventory.transform.position;
                    Vector2 inv = new Vector2(mc.inventory.transform.position.x + 50, mc.inventory.transform.position.y);
                    mc.inventory.transform.position = inv;

                    mc.inventory.SetActive(true);

                    while (true)
                    {
                        try
                        {
                            Destroy(GameObject.FindWithTag("asa"));
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
        }
        else
        {
            Tezgah.SetActive(false);
        }

    }
}
