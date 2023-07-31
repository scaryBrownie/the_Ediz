using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToVillageFromRoom : MonoBehaviour
{
    public float x;
    public float y;
    public int sceneIndex;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("Player"));
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(x, y, 0);
            SceneMovement.DontDestroyOnLoad(GameObject.FindWithTag("MainCamera"));
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
