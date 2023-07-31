using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gecisler : MonoBehaviour
{
    public AudioSource click;



    public void Quit()
    {
        click.Play();
        Application.Quit();
    }
}
