using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaParent : MonoBehaviour{

    public AudioSource bomb;

    void Start()
    {
        bomb.Play();
        Destroy(gameObject,1.55f);
    }
}
