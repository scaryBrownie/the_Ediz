using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class saveCollider : MonoBehaviour
{
    public GameObject text;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<mcScript>().saveBool = true;
            text.GetComponent<TextMeshProUGUI>().text = "Press 'E' to save.";
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            text.GetComponent<TextMeshProUGUI>().text = "";
            collision.gameObject.GetComponent<mcScript>().saveBool = false;
        }
    }
}
