using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class SceneMovement : MonoBehaviour
{
    public float x;
    public float y;
    public int sceneIndex;
    Animator anim;
    bool buldum;
    int v;

    public AudioSource click;

    void Start()
    {
        v = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(bekleee());
        }
        else if (collision.gameObject.tag == "Player" && gameObject.name == "kapii_0")
        {
            mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
            mc.talkJenny2ndisOver = false;
            StartCoroutine(bekleee());
        }
    }
    IEnumerator bekleee()
    {
        mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();

        anim.SetTrigger("start");

        yield return new WaitForSeconds(0.5f);

        SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("Player"));
        GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(x, y, 0);
        SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("MainCamera"));
        SceneManager.LoadScene(sceneIndex);

        if (sceneIndex == 9)
        {
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().Follow = default;
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().LookAt = default;
            GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position 
                = new Vector3(117.0272f, 56.51943f, -10);
            mc.isOnDungeon = true;
        }
        else if (sceneIndex == 2)
        {
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().Follow
                = GameObject.FindWithTag("Player").transform;
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().LookAt
                = GameObject.FindWithTag("Player").transform;
            mc.isOnDungeon = false;
        }
        else if (sceneIndex == 7)
        {
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().Follow = default;
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().LookAt = default;
            GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position
                = new Vector3(74.62f, 34.3f, -10);
            mc.isOnDungeon = true;
        }

        anim.SetTrigger("end");
        anim.SetBool("idleBack", true);
    }


    public void Playy()
    {
        click.Play();
        SceneManager.LoadScene(8);
    }

    IEnumerator bekleAzBe()
    {
        yield return new WaitForSeconds(1.5f);
    }

    
}