using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToSampleScene : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("Player"));
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(-1.719672f, -3.886889f, 0);
            SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("MainCamera"));
            SceneManager.LoadScene(1);
        }
    }
}
