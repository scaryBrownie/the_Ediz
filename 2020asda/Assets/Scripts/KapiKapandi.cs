using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapiKapandi : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
