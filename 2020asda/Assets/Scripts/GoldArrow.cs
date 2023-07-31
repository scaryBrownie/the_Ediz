using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldArrow : MonoBehaviour
{
    Rigidbody2D rb;
    public float X;
    public float Y;
    public float Damage;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(X, Y, 0);
        Destroy(gameObject, 5f);
        this.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "skeleton(Clone)" || collision.gameObject.name == "skeleton")
        {
            collision.gameObject.GetComponent<skeletonScript>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.05f);
            Destroy(gameObject);
            this.enabled = false;
        }
        if (collision.gameObject.name == "EnemyMole(Clone)" || collision.gameObject.name == "EnemyMole")
        {
            collision.gameObject.GetComponent<miniEnemies>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
        if (collision.gameObject.name == "slime(Clone)" || collision.gameObject.name == "slime")
        {
            collision.gameObject.GetComponent<slime>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
        if (collision.gameObject.name == "EnemyAgac(Clone)" || collision.gameObject.name == "EnemyAgac")
        {
            collision.gameObject.GetComponent<AgacEnemy>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
        if (collision.gameObject.name == "yarasa(Clone)" || collision.gameObject.name == "yarasa")
        {
            collision.gameObject.GetComponent<yarasa>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
        if (collision.gameObject.name == "SlimeBoss")
        {
            collision.gameObject.GetComponent<slimeKing>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
        if (collision.gameObject.name == "SlimeMinik(Clone)" || collision.gameObject.name == "SlimeMinik")
        {
            collision.gameObject.GetComponent<slime>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
        if (collision.gameObject.name == "Necromancer(Clone)" || collision.gameObject.name == "Necromancer")
        {
            collision.gameObject.GetComponent<Necromancer>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.gameObject.name == "Jenny(Clone)" && mc.mcAttackJenny || collision.gameObject.name == "Jenny" && mc.mcAttackJenny)
        {
            collision.gameObject.GetComponent<Jenny>().Zehir(Damage);
            cameraShake.Instance.CamShake(1, 2, 0.03f);
            Destroy(gameObject);
            this.enabled = false;
        }
    }
}
