using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class slimeKing : MonoBehaviour
{

    private float hiz;
    Animator animator;
    public float can;
    Transform targetTransform;
    public GameObject goz, kapi;
    public GameObject[] slimes;

    private Color matDefault;
    SpriteRenderer sr;
    public GameObject[] items;
    public float followMCrange;
    public float pushBackRange;
    float a, b, zaman;
    public float speed;
    public bool speedBool;
    public ParticleSystem doorcrash;
    public float damageMC;

    public ParticleSystem slimecogal, ok, ok1;

    void Start()
    {
        zaman = Time.time;

        animator = gameObject.GetComponent<Animator>();

        sr = gameObject.GetComponent<SpriteRenderer>();
        matDefault = sr.color;
        b = speed;
        a = speed + speed;
    }

    void Update()
    {
        FollowMC();
        if (can <= 50)
        {
            speedBool = true;
        }
        else
        {
            speedBool = false;
        }
        speedAyar();
    }



    private void FollowMC()
    {
        targetTransform = GameObject.FindWithTag("Player").transform;
        if (targetTransform.localPosition.x - transform.localPosition.x <= 10f && targetTransform.localPosition.y - transform.localPosition.y <= 10f || transform.localPosition.x - targetTransform.localPosition.x <= 10f && transform.localPosition.y - targetTransform.localPosition.y <= 10f)
        {
            asd(true);
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, hiz * Time.deltaTime);
            animator.SetFloat("Horizontal", (targetTransform.position.x - transform.position.x));
            animator.SetFloat("Vertical", (targetTransform.position.y - transform.position.y));
            if (targetTransform.position.x - transform.position.x <= followMCrange
                && targetTransform.position.x - transform.position.x >= -followMCrange
                && targetTransform.position.y - transform.position.y <= followMCrange
                && targetTransform.position.y - transform.position.y >= -followMCrange
                || transform.position.x - targetTransform.position.x <= followMCrange
                && transform.position.x - targetTransform.position.x >= -followMCrange
                && transform.position.y - targetTransform.position.y <= followMCrange
                && transform.position.y - targetTransform.position.y >= -followMCrange)
            {
                asd(false);
                hiz = 0;
                if (Time.time > zaman)
                {
                    mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
                    mc.toUsSlmAttack.Play();

                    int i = Random.Range(0, slimes.Length);
                    slimecogal.Play();
                    Instantiate(slimes[i], gameObject.transform.position, Quaternion.identity);
                    Instantiate(slimes[i], gameObject.transform.position, Quaternion.identity);
                    zaman = Time.time + 1.5f;
                }

                asd(false);
                hiz = 0;
            }
            else
            {
                asd(true);
                hiz = speed;
            }
        }
        else
        {
            asd(false);
        }
    }
    void asd(bool a)
    {
        if (can > 50)
        {
            animator.SetBool("red", false);
            animator.SetBool("isMoving", a);
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("red", a);
        }
    }

    public void TakeDamage(float dmg)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        mc.toSlmAttack.Play();

        sr.color = Color.red;
        Invoke("ResetColor", 0.15f);
        can -= dmg;
        if (can <= 0)
        {
            mc.goBackkDungeon = false;
            kapi.SetActive(true);
            animator.SetBool("isMoving", false);
            GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the Saffron and talk to her.";
            mc.bekleUla(2);
            animator.SetTrigger("death");
            this.enabled = false;
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], gameObject.transform.position, Quaternion.identity);
            Instantiate(goz, gameObject.transform.position, Quaternion.identity);
            doorcrash.Play();
            cameraShake.Instance.CamShake(2, 2, 0.5f);
            mc.goSaffron = true;
            mc.birdn = false;
            Destroy(gameObject, 0.25f);
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


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<mcScript>().TakeDamage(8);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InvokeRepeating("GiveDamage", 1f, 1f);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CancelInvoke();
        }
    }
    private void GiveDamage()
    {
        GameObject.FindWithTag("Player").GetComponent<mcScript>().TakeDamage(damageMC);
    }

    void speedAyar()
    {
        if (speedBool)
        {
            speed = a;  
        }
        if (!speedBool)
        {
            speed = b;
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
            kapi.SetActive(true);
            animator.SetBool("isMoving", false);
            GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the Saffron and talk to her.";
            mc.bekleUla(2);
            animator.SetTrigger("death");
            this.enabled = false;
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], gameObject.transform.position, Quaternion.identity);
            Instantiate(goz, gameObject.transform.position, Quaternion.identity);
            doorcrash.Play();
            cameraShake.Instance.CamShake(2, 2, 0.5f);
            mc.goSaffron = true;
            mc.birdn = false;
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

        ok.Play();

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
            kapi.SetActive(true);
            animator.SetBool("isMoving", false);
            GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the Saffron and talk to her.";
            mc.bekleUla(2);
            animator.SetTrigger("death");
            this.enabled = false;
            int randomIndex = Random.Range(0, items.Length);
            Instantiate(items[randomIndex], gameObject.transform.position, Quaternion.identity);
            Instantiate(goz, gameObject.transform.position, Quaternion.identity);
            doorcrash.Play();
            cameraShake.Instance.CamShake(2, 2, 0.5f);
            mc.goSaffron = true;
            mc.birdn = false;
            
            Destroy(gameObject, 0.25f);
        }
    }
}
