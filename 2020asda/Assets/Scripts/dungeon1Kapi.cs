using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeon1Kapi : MonoBehaviour
{ 
    public float camX, camY;
    bool allEnmDown;
    public GameObject enemy;

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
            if (gameObject.name == "Kapi3 (2)")
            {
                GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position
                    = new Vector3(camX, camY, -10);
                enemy.transform.position = new Vector2(28.3f, 5.9f);
            }
            else
            {
                GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position
                    = new Vector3(camX, camY, -10);
                try
                {
                    enemy.SetActive(true);
                }
                catch (Exception)
                {
                    Debug.Log("Clear");
                }
            }
        }
    }
}
