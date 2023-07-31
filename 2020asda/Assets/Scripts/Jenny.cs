using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Jenny : MonoBehaviour
{
    public GameObject pressImg;
    bool a;

    Animator animator;
    public Animator bridgeAnim;

    Transform targetTransform;

    public GameObject corona, saffron, kopru;
    SpriteRenderer sr;
    Color matDefault;

    public PlayableDirector saffronGelAma;

    public ParticleSystem coronaSpawner, kopru1, kopru2, ok, ok1;

    public bool talkover;

    public float followMCrange, speed, can;
    public BoxCollider2D trigger;
    float hiz, zaman;

    void Start()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.jennyActive)
        {
            mc.jennyActive = false;
            Destroy(gameObject);
        }


        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        hiz = speed;
        matDefault = sr.color;
        a = true;
    }

    void Update()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (mc.isOnJenny == true && Input.GetKeyDown(KeyCode.E) && mc.ucuncuGorev == true)
        {
            mc.click.Play();

            mc.hiz = 0;
            GameObject.Find("JN").GetComponent<DialougeTrigger>().TriggerDialog();
            a = false;
        }
        else if (mc.isOnJenny == true && mc.onuncugorev == true && Input.GetKeyDown(KeyCode.E))
        {
            mc.click.Play();

            mc.hiz = 0;
            GameObject.Find("onReplik").GetComponent<DialougeTrigger>().TriggerDialog();
        }
        else if (mc.isOnJenny == true && mc.onikincigorev == true && Input.GetKeyDown(KeyCode.E))
        {
            mc.click.Play();

            mc.hiz = 0;
            GameObject.Find("Jenny1").GetComponent<DialougeTrigger>().TriggerDialog();
        }
        if (talkover)
        {
            pressImg.SetActive(false);

            FollowMC();
        }
        else
        {
            targetTransform = GameObject.FindWithTag("Player").transform;
            animator.SetFloat("Horizontal", (targetTransform.position.x - transform.position.x));
            animator.SetFloat("Vertical", (targetTransform.position.y - transform.position.y));
        }
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
            if (targetTransform.position.x - transform.position.x <= followMCrange
                && targetTransform.position.x - transform.position.x >= -followMCrange
                && targetTransform.position.y - transform.position.y <= followMCrange
                && targetTransform.position.y - transform.position.y >= -followMCrange
                || transform.position.x - targetTransform.position.x <= followMCrange
                && transform.position.x - targetTransform.position.x >= -followMCrange
                && transform.position.y - targetTransform.position.y <= followMCrange
                && transform.position.y - targetTransform.position.y >= -followMCrange)
            {
                animator.SetBool("isMoving", false);
                hiz = 0;
                if (Time.time > zaman)
                {
                    coronaSpawner.Play();
                    Instantiate(corona, gameObject.transform.position, Quaternion.identity);
                    zaman = Time.time + 1;
                }

                hiz = 0;
            }
            else
            {
                animator.SetBool("isMoving", true);
                hiz = speed;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void TakeDamage(float dmg)
    {
        sr.color = Color.red;
        Invoke("ResetColor", 0.15f);
        can -= dmg;
        if (can <= 0)
        {
            animator.SetBool("isMoving", false);
            this.enabled = false;
            animator.SetTrigger("death");
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            StartCoroutine(SaffronGel());
        }
    }
    void ResetColor()
    {
        sr.color = matDefault;
    }

    IEnumerator SaffronGel()
    {
        GameObject.FindWithTag("Player").GetComponent<mcScript>().hiz = 0;

        GameObject.FindWithTag("Player").GetComponent<mcScript>().animator.SetTrigger("start");
        saffron.SetActive(true);
        saffron.GetComponent<Saffron>().textControl = true;
        yield return new WaitForSeconds(1);
        GameObject.FindWithTag("Player").transform.position = new Vector2(1.16f, 1.48f);
        GameObject.FindWithTag("Player").GetComponent<mcScript>().animator.SetTrigger("end");
        yield return new WaitForSeconds(1);
        saffronGelAma.Play();
        cameraShake.Instance.CamShake(5, 1, 3);
        kopru1.Play();
        kopru2.Play();
        bridgeAnim.SetTrigger("yikil");
        yield return new WaitForSeconds(4);
        kopru.SetActive(false);
        saffronGelAma.Play();
    }

    public void DestroyCollider()
    {
        Destroy(trigger);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.CompareTag("Player") && a && !talkover)
        {
            mc.isOnJenny = true;
            pressImg.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        if (collision.gameObject.tag == "Player")
        {
            mc.isOnJenny = false;
            pressImg.SetActive(false);
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

        yield return new WaitForSeconds(0.6f);

        ok.Play();
        can -= 1;


        gameObject.GetComponent<SpriteRenderer>().color = matDefault;




        if (can <= 0)
        {
            animator.SetBool("isMoving", false);
            animator.SetTrigger("death");
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            StartCoroutine(SaffronGel()); ;
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
            animator.SetTrigger("death");
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            StartCoroutine(SaffronGel());;
            this.enabled = false;
        }
    }
}
