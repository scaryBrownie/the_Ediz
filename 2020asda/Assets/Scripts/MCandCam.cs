using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCandCam : MonoBehaviour
{
    bool MCisHere;
    public GameObject mc;
    public GameObject Cam;
    GameObject CamScript;

    void Start()
    {
        try
        {
           GameObject a = GameObject.FindWithTag("findCam");
            Debug.Log(a.transform.position.ToString());
        }
        catch (NullReferenceException)
        {
            Instantiate(mc, gameObject.transform.localPosition, Quaternion.identity);
            Instantiate(Cam, gameObject.transform.localPosition, Quaternion.identity);
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").GetComponent<Transform>();
            GameObject.FindWithTag("findCam").GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }
}
