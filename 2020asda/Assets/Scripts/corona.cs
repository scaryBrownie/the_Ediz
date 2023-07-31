using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corona : MonoBehaviour
{
    mcScript mc;

    public float damageMC;

    public float hiz;
    float zaman, a;
    void Start()
    {
        a = 0;
        mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        Destroy(gameObject, 3);
        zaman = Time.time;
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, hiz * Time.deltaTime);


        if (Time.time > zaman)
        {
            a += 1;
            transform.Rotate(new Vector3(0, 0, a));
            zaman = Time.time + 0.08f;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mc.TakeDamage(damageMC);
            Destroy(gameObject);
        }
    }
}
