using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
public class Necromancer : MonoBehaviour
{
    public float hiz;
    public Animator animator;
    public float can, damagetoMC;
    Transform targetTransform;
    public GameObject skeleton;

    private Color matDefault;
    SpriteRenderer sr;
    public GameObject[] items;
    public float followMCrange, zaman;
    public float pushBackRange;

    public GameObject alev, kapi;
    public BoxCollider2D meet;
    public bool talkisover;

    public ParticleSystem doorcrash, ok, ok1, buyu;

    float time;
    bool c, oldunyeter;
    void Start()
    {
        oldunyeter = talkisover = false;
        time = zaman = Time.time;

        animator = gameObject.GetComponent<Animator>();

        sr = gameObject.GetComponent<SpriteRenderer>();
        matDefault = sr.color;
    }

    void Update()
    {
        if (talkisover)
        {
            FollowMC();
        }
        else
        {
            GameObject mc = GameObject.FindWithTag("Player");
            animator.SetFloat("Horizontal", mc.transform.position.x - gameObject.transform.position.x);
            animator.SetFloat("Vertical", mc.transform.position.y - gameObject.transform.position.y);
        }
        
    }



    private void FollowMC()
    {
        targetTransform = GameObject.FindWithTag("Player").transform;
        if (targetTransform.localPosition.x - transform.localPosition.x <= 10f && targetTransform.localPosition.y - transform.localPosition.y <= 10f 
            || transform.localPosition.x - targetTransform.localPosition.x <= 10f && transform.localPosition.y - targetTransform.localPosition.y <= 10f)
        {
            animator.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, hiz * Time.deltaTime);
            animator.SetFloat("Horizontal", (targetTransform.position.x - transform.position.x));
            animator.SetFloat("Vertical", (targetTransform.position.y - transform.position.y));
            if (targetTransform.position.x - transform.position.x <= followMCrange && targetTransform.position.x - transform.position.x >= -followMCrange && targetTransform.position.y - transform.position.y <= followMCrange && targetTransform.position.y - transform.position.y >= -followMCrange || transform.position.x - targetTransform.position.x <= followMCrange && transform.position.x - targetTransform.position.x >= -followMCrange && transform.position.y - targetTransform.position.y <= followMCrange && transform.position.y - targetTransform.position.y >= -followMCrange)
            {
                if (Time.time > zaman)
                {
                    animator.SetBool("isMoving", false);
                    hiz = 0;
                    StartCoroutine(damageMC());
                    zaman = Time.time + 4;
                }
                if (Time.time > time)
                {
                    Instantiate(alev, gameObject.transform.position, Quaternion.identity);
                    time = Time.time + 1;
                }
            }
            else
            {
                hiz = 2.5f;
                c = true;
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            GameObject mc = GameObject.FindWithTag("Player");
            animator.SetBool("isMoving", false);
            animator.SetFloat("Horizontal", mc.transform.position.x - gameObject.transform.position.x);
            animator.SetFloat("Vertical", mc.transform.position.y - gameObject.transform.position.y);
        }
    }

    public void TakeDamage(float dmg)
    {
        sr.color = Color.red;
        Invoke("ResetColor", 0.15f);
        can -= dmg;
        if (can <= 0 && !oldunyeter)
        {
            oldunyeter = true;
            mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
            mc.goBackkDungeon = false;
            mc.birdn = mc.ikidn = false;
            mc.ucdn = true;
            animator.SetBool("isMoving", false);
            animator.SetTrigger("death");
            this.enabled = false;
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], gameObject.transform.position, Quaternion.identity);
            cameraShake.Instance.CamShake(3f, 5, 0.5f);
            doorcrash.Play();
            kapi.SetActive(true);
            GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Talk to Jenny.";

            mc.bekleUla(10);
            mc.necromancerDead = true;
            Destroy(gameObject, 0.25f);
        }
    }

    void ResetColor()
    {
        sr.color = matDefault;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.gameObject.tag == "okyukari" && talkisover)
        {
            gameObject.transform.Translate(0, pushBackRange, 0);
        }
        if (collision.gameObject.tag == "okassa" && talkisover)
        {
            gameObject.transform.Translate(0, -pushBackRange, 0);
        }
        if (collision.gameObject.tag == "oksol" && talkisover)
        {
            gameObject.transform.Translate(-pushBackRange, 0, 0);
        }
        if (collision.gameObject.tag == "oksag" && talkisover)
        {
            gameObject.transform.Translate(pushBackRange, 0, 0);
        }
        if (collision.gameObject.tag == "Player" && mc.dokuzuncugorev)
        {
            mc.hiz = 0;
            GameObject.Find("NC").GetComponent<DialougeTrigger>().TriggerDialog();
            Destroy(meet);
        }
    }

    IEnumerator damageMC()
    {
        if (c)
        {
            yield return new WaitForSeconds(1);

            float s = Random.Range(1, 3);
            float a = 0;
            while (true)
            {
                if (a == s)
                {
                    break;
                }
                buyu.Play();
                Instantiate(skeleton, gameObject.transform.position, Quaternion.identity);
                a++;
            }
            c = false;
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
            mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
            mc.goBackkDungeon = false;
            mc.birdn = mc.ikidn = false;
            mc.ucdn = true;
            animator.SetBool("isMoving", false);
            animator.SetTrigger("death");
            this.enabled = false;
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], gameObject.transform.position, Quaternion.identity);
            cameraShake.Instance.CamShake(3f, 5, 0.5f);
            doorcrash.Play();
            kapi.SetActive(true);
            GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Talk to Jenny.";
            mc.bekleUla(10);
            mc.necromancerDead = true;
            Destroy(gameObject, 0.25f);
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
            mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
            mc.goBackkDungeon = false;
            mc.birdn = mc.ikidn = false;
            mc.ucdn = true;
            animator.SetBool("isMoving", false);
            animator.SetTrigger("death");
            this.enabled = false;
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], gameObject.transform.position, Quaternion.identity);
            cameraShake.Instance.CamShake(3f, 5, 0.5f);
            doorcrash.Play();
            kapi.SetActive(true);
            GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Talk to Jenny.";
            mc.bekleUla(10);
            mc.necromancerDead = true;
            Destroy(gameObject, 0.25f);
        }
    }
}
