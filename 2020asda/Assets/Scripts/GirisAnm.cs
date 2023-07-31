using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GirisAnm : MonoBehaviour
{
    public PlayableDirector sfCome;
    GameObject mc;
    public GameObject pressImg;

    bool ucandouit;


    void Start()
    {
    }
    void Update()
    {
        if (ucandouit && Input.GetKeyDown(KeyCode.E))
        {
            mcScript mc1 = GameObject.FindWithTag("Player").GetComponent<mcScript>();
            mc1.click.Play();

            pressImg.SetActive(false);
            mc = GameObject.FindWithTag("Player");
            mc.GetComponent<mcScript>().hiz = 0;
            sfCome.Play();
            GameObject.Find("Saffron").GetComponent<Saffron>().PressImage.color = Color.clear;
            GameObject.Find("Saffron").GetComponent<Saffron>().PressText.text = "";
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressImg.SetActive(true);
            mc = GameObject.FindWithTag("Player");
            
            if (mc.GetComponent<mcScript>().ilkGorev == true)
            {
                ucandouit = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ucandouit = false;
            pressImg.SetActive(false);
        }
    }
}
