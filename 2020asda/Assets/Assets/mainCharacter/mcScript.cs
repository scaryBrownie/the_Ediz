using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.U2D;
using System;
using Cinemachine;
using UnityEngine.Audio;

public class mcScript : MonoBehaviour
{
    public string missionTxT;

    public ParticleSystem RedPotion, BluePotion, GreenPotion, eatparticle, die, Yikil1, Yikil2, Yikil3, Yikil4;

    public float hiz;
    public float playerScale = 7;
    public GameObject DownArrow, GoldDownArrow, DarkDownArrow, GreenDownArrow;
    public GameObject UpArrow, GoldUpArrow, DarkUpArrow, GreenUpArrow;
    public GameObject RightArrow, GoldRightArrow, DarkRightArrow, GreenRightArrow;
    public GameObject LeftArrow, GoldLeftArrow, DarkLeftArrow, GreenLeftArrow;
    public Animator animator;
    SpriteRenderer sr;
    Color defaultColor;
    public bool inventoryToggle, saveBool;
    public GameObject inventory;

    public UnityEngine.UI.Image HP, MP;
    public int maxHealth, maxMP, sceneIndex;
    public float currentHealth, currentMP;

    public bool isOnPepper;
    public bool isOnDealer;
    public bool isOnSaffron;
    public bool isOnJenny;
    public bool isOnDungeon;
    public bool ilkGorev, ikinciGorev, ucuncuGorev, dorduncuGorev, besinciGorev, altincigorev,
        yedincigorev, sekizincigorev, dokuzuncugorev, onuncugorev, onbirincigorev, onikincigorev, onUcuncuGorev, onDorduncuGorev,
        goSaffron, goWP, goPepper, birdn, ikidn, ucdn, talkJenny2ndisOver, mcAttackJenny, goBackkDungeon, jennyActive, necromancerDead;

    public float money;
    public TextMeshProUGUI moneyText, noticeText;

    public UnityEngine.UI.Image ArrowGreen, ArrowGold, ArrowDark, ArrowNormal;

    public GameObject Notice;

    public Sprite Pepper, Saffron, Jenny, Necromancer, You;

    public UnityEngine.UI.Image Resim;

    public GameObject dialougeText, deathPanel, StarvingDeathPanel, stoppanel, hpDeathPanel;

    public DayCycles dayCycle;

    public AudioSource coin, drinkPot, eatMeal, arrowGiy, toSlmAttack, toUsSlmAttack, okfip, attacktoSkeleton, click, throwItem, pickItem;

    float timee, mpTime, DaycycleTimee;
    bool stoppanelbool;
    public int noticeInt, agacInt;

    public float cycleCurrentTime, cycleMaxTime;

    [SerializeField]
    public bool sol, sag, alt, ust, stop;


    void Start()
    {
        mcAttackJenny = false;
        stoppanelbool = birdn = ikidn = ucdn = true;
        cycleCurrentTime = agacInt = 0;


        noticeInt = 1;
        StartCoroutine(bekle(1));


        ilkGorev = true;
        ikinciGorev = ucuncuGorev = dorduncuGorev = besinciGorev = false;


        currentHealth = maxHealth;
        currentMP = maxMP;


        inventoryToggle = true;

        animator = gameObject.GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

        defaultColor = sr.color;

        ArrowNormal.enabled = ArrowDark.enabled = ArrowGold.enabled = ArrowGreen.enabled = false;
        DaycycleTimee = timee = mpTime = Time.time;
    }


    void Update()
    {
        Movement(); AttackCharacter(); noticeee(); talkBoxControl(); geceGunduz(); UyuwSave();


        moneyText.text = money + "$";

        sceneIndex = SceneManager.GetActiveScene().buildIndex;


        HP.fillAmount = currentHealth / maxHealth;
        MP.fillAmount = currentMP / maxMP;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        else if (currentMP > 100)
        {
            currentMP = 100;
        }
        if (agacInt == 12)
        {
            bekleUla(5);
            agacInt = 0;
            GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = "Go to the Pepper and talk to her.";
            GameObject.Find("Control6.ms").GetComponent<sixMission>().missionOver6 = true;
            goPepper = true;
        }
    }
    public void Continue()
    {
        click.Play();
        stoppanel.SetActive(false);
        Time.timeScale = 1;
        stoppanelbool = !stoppanelbool;
    }
    void LateUpdate()
    {
        if (Time.time > mpTime)
        {
            currentMP -= 1;
            if (currentMP <= 0)
            {
                Time.timeScale = 0f;
                StarvingDeathPanel.SetActive(true);
            }
            mpTime = Time.time + 5f;
        }
    }
    public void TakeDamage(float damage)
    {
        cameraShake.Instance.CamShake(1, 2, 0.03f);
        currentHealth -= damage;
        if (currentHealth >= 6)
        {
            StartCoroutine(colorDelay());
        }
        else if (currentHealth <= 0)
        {
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().Follow =
                gameObject.transform;
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().LookAt =
                gameObject.transform;
            animator.SetTrigger("death");
            hpDeathPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void geceGunduz()
    {
        cycleCurrentTime += Time.deltaTime;
        try
        {
            GameObject.Find("Light").GetComponent<DayNightSystem2D>().dayCycle = dayCycle;
        }
        catch (Exception)
        {
        }

        if (cycleCurrentTime >= cycleMaxTime)
        {
            cycleCurrentTime = 0;
            try
            {
                GameObject.Find("Light").GetComponent<DayNightSystem2D>().dayCycle++;
                if (GameObject.Find("Light").GetComponent<DayNightSystem2D>().dayCycle > DayCycles.Midnight)
                    GameObject.Find("Light").GetComponent<DayNightSystem2D>().dayCycle = 0;

                dayCycle = GameObject.Find("Light").GetComponent<DayNightSystem2D>().dayCycle;
            }
            catch (Exception)
            {
            }
        }

        try
        {
            GameObject.Find("Light").GetComponent<DayNightSystem2D>().cycleCurrentTime = cycleCurrentTime;
            GameObject.Find("Light").GetComponent<DayNightSystem2D>().cycleMaxTime = cycleMaxTime;
        }
        catch (Exception)
        {
        }
    }
    void ResetColor()
    {
        sr.color = defaultColor;
    }
    IEnumerator colorDelay()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = defaultColor;
        yield return new WaitForSeconds(0.05f);

        sr.color = Color.yellow;
        yield return new WaitForSeconds(0.1f);
        sr.color = defaultColor;
        yield return new WaitForSeconds(0.05f);

        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = defaultColor;
        yield return new WaitForSeconds(0.05f);

        sr.color = Color.clear;
        yield return new WaitForSeconds(0.07f);
        sr.color = defaultColor;
        yield return new WaitForSeconds(0.05f);

        sr.color = Color.clear;
        yield return new WaitForSeconds(0.07f);
        sr.color = defaultColor;
        yield return new WaitForSeconds(0.05f);

        sr.color = Color.clear;
        yield return new WaitForSeconds(0.07f);
        sr.color = defaultColor;
        yield return new WaitForSeconds(0.05f);
    }



    public void noticeee()
    {
        if (noticeInt == 1)
        {
            noticeText.text = " For years I have traveled from village to village and went on adventures." +
            " I still don't remember anything of myself.\n This journey brought me to this village." +
            " However, life in this village seems extinct." +
            " I have to find the task signboard to understand what is happening.";
        }
        else if (noticeInt == 2)
        {
            noticeText.text = " What this girl said confirms what I see." +
                " I can't stop doing nothing." +
                " I have to do something to make this village come alive.\n" +
                " Now I must go to the dark forest and bring what Saffron wants.\n" +
                " The villagers told me to follow the village road to the end so I could go into the dark forest.";
        }
        else if (noticeInt == 3)
        {
            noticeText.text = "I shouldn't trust Jenny." +
                " It is not possible to live in the Dark Forest and be a good person." +
                " Moreover, she spoke badly about Saffron.\n\n\n\n" +
                "Note:'If you want to enter the dungeon you have to run to the dungeon door.'";
        }
        else if (noticeInt == 4)
        {
            noticeText.text = "I got what the Saffron wanted. I must go to Saffron without wasting time.";
        }
        else if (noticeInt == 5)
        {
            noticeText.text = "Pepper is the best known merchant in this village." +
                " The villagers said that I could find Pepper when I turn right from the statues in the village center.";
        }
        else if (noticeInt == 6)
        {
            noticeText.text = "I can sell the items I dropped from monsters to Saffron," +
                " Pepper or the Weapon Dealer to increase my money and buy myself weapons " +
                "and pots.\n\n\n Now I must go back to the dark forest and cut down the monsters that haunt the field.";
        }
        else if (noticeInt == 7)
        {
            noticeText.text = "I cleared the monsters around the field." +
                " I have to go to Pepper as soon as possible and say that this place is safe.";
        }
        else if (noticeInt == 8)
        {
            noticeText.text = "I have to meet the gun dealer. The award it will give may be beneficial for me. " +
                "\n\n\n She said the weapon dealer shop was in the upper right corner of Pepper's shop.";
        }

        else if (noticeInt == 9)
        {
            noticeText.text = "I feel unstoppable with this arrow." +
                "\n\n\n Let's see if Saffron had done something about the cure.";
        }
        else if (noticeInt == 10)
        {
            noticeText.text = "I have to go back to the dark forest dungeon.";
        }
        else if (noticeInt == 11)
        {
            noticeText.text = "I finally found what Saffron wanted. This thing is heavier than I thought." +
                " I have to get it to Saffron without wasting time.";
        }
        else if (noticeInt == 12)
        {
            noticeText.text = "I knew Jenny was bad person, but I didn't think she'd go this far." +
                "\n\n\n I gotta get to the dark forest dungeon and beat him. So Saffron can use holy water.";
        }
    }

    private void talkBoxControl()
    {
        string control = dialougeText.GetComponent<TextMeshProUGUI>().text;

        if (control.StartsWith("S"))
        {
            Resim.sprite = Saffron;
        }
        else if (control.StartsWith("J"))
        {
            Resim.sprite = Jenny;
        }
        else if (control.StartsWith("P"))
        {
            Resim.sprite = Pepper;
        }
        else if (control.StartsWith("N"))
        {
            Resim.sprite = Necromancer;
        }
        else if (control.StartsWith("Y"))
        {
            Resim.sprite = You;
        }
    }
    private void animATiON()
    {
        if (sol)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("Horizontal", -1);
        }
        else if (sag)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("Horizontal", -1);
        }
        else if (ust)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("Vertical", 1);
        }
        else if (alt)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("Vertical", -1);
        }
        else if (stop)
        {
            animator.SetBool("isMoving", false);
        }
    }





    private void Movement()
    {


        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Time.deltaTime * -hiz, 0, 0);
            animator.SetBool("isMoving", true);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", -1);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isMoving", false);
        }


        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Time.deltaTime * hiz, 0, 0);
            animator.SetBool("isMoving", true);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", 1);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isMoving", false);
        }



        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, Time.deltaTime * hiz, 0);
            animator.SetBool("isMoving", true);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 1);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isMoving", false);
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, Time.deltaTime * -hiz, 0);
            animator.SetBool("isMoving", true);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && stoppanelbool)
        {
            click.Play();
            stoppanel.SetActive(true);
            Time.timeScale = 0;
            stoppanelbool = !stoppanelbool;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !stoppanelbool)
        {
            click.Play();
            stoppanel.SetActive(false);
            Time.timeScale = 1;
            stoppanelbool = !stoppanelbool;
        }

        //inventory on off
        if (Input.GetKeyDown(KeyCode.I) && inventoryToggle)
        {
            inventory.SetActive(true);
            inventoryToggle = !inventoryToggle;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !inventoryToggle)
        {
            inventory.SetActive(false);
            GameObject.Find("HowMuchtoSell").GetComponent<TextMeshProUGUI>().text = "";
            inventoryToggle = !inventoryToggle;
        }
    }


    private void AttackCharacter()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Time.time > timee)
            {
                okfip.Play();

                if (ArrowNormal.enabled)
                {
                    Instantiate(DownArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGreen.enabled)
                {
                    Instantiate(GreenDownArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGold.enabled)
                {
                    Instantiate(GoldDownArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowDark.enabled)
                {
                    Instantiate(DarkDownArrow, transform.position, Quaternion.identity);
                }
                timee = Time.time + 0.2f;
            }

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Time.time > timee)
            {
                okfip.Play();

                if (ArrowNormal.enabled)
                {
                    Instantiate(UpArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGreen.enabled)
                {
                    Instantiate(GreenUpArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGold.enabled)
                {
                    Instantiate(GoldUpArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowDark.enabled)
                {
                    Instantiate(DarkUpArrow, transform.position, Quaternion.identity);
                }
                timee = Time.time + 0.2f;
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Time.time > timee)
            {
                okfip.Play();

                if (ArrowNormal.enabled)
                {
                    Instantiate(RightArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGreen.enabled)
                {
                    Instantiate(GreenRightArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGold.enabled)
                {
                    Instantiate(GoldRightArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowDark.enabled)
                {
                    Instantiate(DarkRightArrow, transform.position, Quaternion.identity);
                }
                timee = Time.time + 0.2f;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Time.time > timee)
            {
                okfip.Play();

                if (ArrowNormal.enabled)
                {
                    Instantiate(LeftArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGreen.enabled)
                {
                    Instantiate(GreenLeftArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowGold.enabled)
                {
                    Instantiate(GoldLeftArrow, transform.position, Quaternion.identity);
                }
                else if (ArrowDark.enabled)
                {
                    Instantiate(DarkLeftArrow, transform.position, Quaternion.identity);
                }
                timee = Time.time + 0.2f;
            }
        }
    }

    public IEnumerator bekle(float time)
    {
        yield return new WaitForSeconds(time);
        Notice.SetActive(true);
        Time.timeScale = 0;
    }

    public void bekleUla(float time)
    {
        StartCoroutine(bekle(time));
    }



    public void redParticle()
    {
        RedPotion.Play();
    }
    public void blueParticle()
    {
        BluePotion.Play();
    }
    public void greenParticle()
    {
        GreenPotion.Play();
    }
    public void closeNotice()
    {
        click.Play();
        noticeText.text = "";
        Notice.SetActive(false);
        Time.timeScale = 1;
        noticeInt++;
    }

    public void BacktoMainMenu()
    {
        deathPanel.SetActive(false);
        StarvingDeathPanel.SetActive(false);
        hpDeathPanel.SetActive(false);

        click.Play();
        SceneManager.LoadScene(0);
    }

    private void UyuwSave()
    {
        if (Input.GetKeyDown(KeyCode.E) && saveBool)
        {
            StartCoroutine(savele());
        }
    }
    IEnumerator savele()
    {
        animator.SetTrigger("start");

        yield return new WaitForSeconds(0.5f);
        SavePlayer();
        animator.SetTrigger("end");

        GameObject.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Saved succes.";
        yield return new WaitForSeconds(1);
        GameObject.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "";
    }
    public void SavePlayer()
    {
        missionTxT = GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text;
        SaveSystem.SaveMC(this);
    }
    public void LoadPlayer()
    {
        click.Play();

        GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().Follow =
            gameObject.transform;
        GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().LookAt =
            gameObject.transform;

        hpDeathPanel.SetActive(false);
        StarvingDeathPanel.SetActive(false);
        stoppanel.SetActive(false);

        MCSaveData data = SaveSystem.LoadMC();

        sceneIndex = data.sceneIndex;
        SceneManager.LoadScene(sceneIndex);
        SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("Player"));
        SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("MainCamera"));

        currentHealth = data.HP;
        currentMP = data.MP;

        money = data.money;

        noticeInt = data.noticeInt;
        GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>().text = data.missionTxT;

        //inventory = data.inventory;

        Vector2 pos;
        pos.x = data.position[0];
        pos.y = data.position[1];
        transform.position = pos;

        dayCycle = data.dayCycle;
        cycleCurrentTime = data.cycleCurrentTime;
        cycleMaxTime = data.cycleMaxTime;

        isOnSaffron = data.isOnSaffron;
        isOnDealer = data.isOnDealer;
        isOnPepper = data.isOnPepper;
        isOnJenny = data.isOnJenny;
        isOnDungeon = data.isOnDungeon;

        goSaffron = data.goSaffron;
        goWP = data.goWP;
        goPepper = data.goPepper;

        ilkGorev = data.ilkGorev;
        ikinciGorev = data.ikinciGorev;
        ucuncuGorev = data.ucuncuGorev;
        dorduncuGorev = data.dorduncuGorev;
        besinciGorev = data.besinciGorev;
        altincigorev = data.altincigorev;
        yedincigorev = data.yedincigorev;
        sekizincigorev = data.sekizincigorev;
        dokuzuncugorev = data.dokuzuncugorev;
        onuncugorev = data.onuncugorev;
        onbirincigorev = data.onbirincigorev;
        onikincigorev = data.onikincigorev;
        onUcuncuGorev = data.onUcuncuGorev;
        onDorduncuGorev = data.onDorduncuGorev;

        birdn = data.birdn;
        ikidn = data.ikidn;
        ucdn = data.ucdn;
        talkJenny2ndisOver = data.talkJenny2ndisOver;
        mcAttackJenny = data.mcAttackJenny;
        goBackkDungeon = data.goBackkDungeon;
        jennyActive = data.jennyActive;

        Time.timeScale = 1;
    }
}


