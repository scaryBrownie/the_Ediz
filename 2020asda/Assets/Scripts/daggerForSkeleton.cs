using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daggerForSkeleton : MonoBehaviour
{
    Rigidbody2D rb;
    public float X;
    Transform target;

    public int damageToMC;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2.5f);
    }

    void Update()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        transform.position = Vector2.MoveTowards(transform.position, target.position, X*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<mcScript>().TakeDamage(damageToMC);
            Destroy(gameObject);
        }
    }
}
