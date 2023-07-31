using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public static cameraShake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float shakeTime;


    Vector3 oldPos;

    void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }


    public void CamShake(float amp,float freq, float time)
    {
        oldPos = GameObject.FindWithTag("MainCamera").transform.position;

        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amp;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = freq;

        shakeTime = time;
    }

    void Update()
    {
        if (shakeTime > 0f)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;

                mcScript mc = GameObject.FindWithTag("Player").GetComponent<mcScript>();
                if (mc.isOnDungeon)
                {
                    GameObject.FindWithTag("MainCamera").transform.position = oldPos;
                }
            }
        }
    }
}
