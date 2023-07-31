using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alev : MonoBehaviour
{
    public float damageTOMC;
    public Necromancer nc;

    float H, V;

    public Animator anim;

    void Start()
    {
        nc = GameObject.Find("Necromancer").GetComponent<Necromancer>();
        H = nc.animator.GetFloat("Horizontal");
        V = nc.animator.GetFloat("Vertical");
        anim.SetFloat("Horizontal", H);
        anim.SetFloat("Vertical", V);

        if (H > 0 && V < H)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 4;
        }
        else if (H < 0 && V > H)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 4;
        }
        else if (V > 0 && V > H)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 4;
        }
        else if (V < 0 && V < H)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 4;
        }

        Destroy(gameObject, 2.5f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<mcScript>().TakeDamage(nc.damagetoMC);
            Destroy(gameObject);
        }
    }
}
