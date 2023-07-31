using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSwordMaster : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("Player"));
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(6.02f, 15.69f, 0);
            SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("MainCamera"));
            SceneManager.LoadScene(4);
        }
    }
}