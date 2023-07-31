using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class skeletonScript : MonoBehaviour
{
    public float hiz;
    Animator animator;
    public float can;
    Transform targetTransform;
    public GameObject bone;
    float bombaBirakmaGecikmesi;

    public Color matDefault;
    SpriteRenderer sr;
    public GameObject[] items;
    public float followMCrange;
    public float pushBackRange;
    public ParticleSystem ok, ok1;

    public AudioSource[] sesler;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        bombaBirakmaGecikmesi = Time.time;

        sr = gameObject.GetComponent<SpriteRenderer>();
        matDefault = sr.color;

        StartCoroutine(sess());
    }

    void Update()
    {
        FollowMC();
        //Physics2D.IgnoreLayerCollision(8, 8);
    }



    private void FollowMC()
    {
        targetTransform = GameObject.FindWithTag("Player").transform;
        if (targetTransform.localPosition.x - transform.localPosition.x <= 10f && targetTransform.localPosition.y - transform.localPosition.y <= 10f || transform.localPosition.x - targetTransform.localPosition.x <= 10f && transform.localPosition.y - targetTransform.localPosition.y <= 10f)
        {
            animator.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, hiz * Time.deltaTime);
            animator.SetFloat("Horizontal", (targetTransform.position.x - transform.position.x));
            animator.SetFloat("Vertical", (targetTransform.position.y - transform.position.y));
            if (targetTransform.position.x - transform.position.x <= followMCrange && targetTransform.position.x - transform.position.x >= -followMCrange && targetTransform.position.y - transform.position.y <= followMCrange && targetTransform.position.y - transform.position.y >= -followMCrange || transform.position.x - targetTransform.position.x <= followMCrange && transform.position.x - targetTransform.position.x >= -followMCrange && transform.position.y - targetTransform.position.y <= followMCrange && transform.position.y - targetTransform.position.y >= -followMCrange)
            {
                animator.SetBool("isMoving", false);
                hiz = 0;
                if (Time.time > bombaBirakmaGecikmesi)
                {
                    animator.SetBool("isAttacking", false);
                    Instantiate(bone, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
                    bombaBirakmaGecikmesi = Time.time + 1.9f;
                    animator.SetBool("isAttacking", true);
                }
            }
            else
            {
                animator.SetBool("isMoving", true);
                animator.SetBool("isAttacking", false);
                hiz = 3;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
        }
    }

    public void TakeDamage(float dmg)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        mc.attacktoSkeleton.Play();

        sr.color = Color.red;
        Invoke("ResetColor", 0.15f);
        can -= dmg;
        if (can <= 0)
        {
            animator.SetBool("isMoving", false);
            Destroy(gameObject, 0.25f);
            animator.SetTrigger("death");
            this.enabled = false;
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], gameObject.transform.position, Quaternion.identity);
        }
    }

    void ResetColor()
    {
        sr.color = matDefault;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "okyukari")
        {
            gameObject.transform.Translate(0, pushBackRange, 0);
        }
        if (collision.gameObject.tag == "okassa")
        {
            gameObject.transform.Translate(0, -pushBackRange, 0);
        }
        if (collision.gameObject.tag == "oksol")
        {
            gameObject.transform.Translate(-pushBackRange, 0, 0);
        }
        if (collision.gameObject.tag == "oksag")
        {
            gameObject.transform.Translate(pushBackRange, 0, 0);
        }
    }
    public void Zehir(float dmg)
    {
        can -= dmg;
        StartCoroutine(beklae());

    }

    IEnumerator beklae()
    {

        ok.Play();

        gameObject.GetComponent<SpriteRenderer>().color = Color.green;


        ok.Play();
        can -= 1;

        gameObject.GetComponent<SpriteRenderer>().color = matDefault;
        yield return new WaitForSeconds(0.07f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        yield return new WaitForSeconds(0.6f);

        ok.Play();
        can -= 1;

        gameObject.GetComponent<SpriteRenderer>().color = matDefault;
        yield return new WaitForSeconds(0.07f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        can -= 1;


        gameObject.GetComponent<SpriteRenderer>().color = matDefault;




        if (can <= 0)
        {
            animator.SetBool("isMoving", false);
            Destroy(gameObject, 0.25f);
            animator.SetTrigger("death");
            this.enabled = false;
        }
    }
    public void BlackZehir(float dmg)
    {
        can -= dmg;
        StartCoroutine(siyahZehir());
    }
    IEnumerator siyahZehir()
    {

        ok1.Play();

        gameObject.GetComponent<SpriteRenderer>().color = Color.black;


        ok1.Play();
        can -= 4;

        gameObject.GetComponent<SpriteRenderer>().color = matDefault;
        yield return new WaitForSeconds(0.07f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;

        yield return new WaitForSeconds(0.6f);

        ok1.Play();
        can -= 4;

        gameObject.GetComponent<SpriteRenderer>().color = matDefault;
        yield return new WaitForSeconds(0.07f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;

        can -= 4;


        gameObject.GetComponent<SpriteRenderer>().color = matDefault;




        if (can <= 0)
        {
            animator.SetBool("isMoving", false);
            Destroy(gameObject, 0.25f);
            animator.SetTrigger("death");
            this.enabled = false;
        }
    }

    IEnumerator sess()
    {
        int i = Random.Range(0, sesler.Length);
        sesler[i].Play();
        yield return new WaitForSeconds(5);
        StartCoroutine(sess());
    }
}
