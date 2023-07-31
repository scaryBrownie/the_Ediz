using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonKapi : MonoBehaviour
{
    public float X, Y;
    public float camX, camY;
    bool allEnmDown;
    Animator anm;
    public GameObject slimeKing;
    IEnumerator anim()
    {
        anm = GameObject.FindWithTag("Player").GetComponent<Animator>();
        anm.SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        GameObject.FindWithTag("Player").transform.position = new Vector2(X, Y);
        GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position
            = new Vector3(camX, camY, -10);
        if (gameObject.name == "kapiSag (2)")
        {
            slimeKing.SetActive(true);
        }
        anm.SetTrigger("end");
    }
    void Update()
    {
        try
        {
            GameObject enm = GameObject.FindWithTag("miniEnemies");
            Debug.Log(enm.name);
            allEnmDown = false;
        }
        catch (NullReferenceException)
        {
            allEnmDown = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && allEnmDown == true)
        {
            StartCoroutine(anim());
        }
    }
}
