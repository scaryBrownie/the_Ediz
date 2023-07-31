using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickup : MonoBehaviour
{
    private inventory inventory;
    public Button itemButton;


    void OnTriggerEnter2D(Collider2D collision)
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<inventory>();

        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < inventory.inventorySlots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    collision.gameObject.GetComponent<mcScript>().pickItem.Play();
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.inventorySlots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
