using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atesSaffron : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Particle System").GetComponent<ParticleSystem>().Play();

            collision.gameObject.GetComponent<mcScript>().TakeDamage(0);

            Destroy(gameObject);
        }
    }
}
