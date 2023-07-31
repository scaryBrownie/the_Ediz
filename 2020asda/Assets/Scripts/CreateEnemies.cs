using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour
{
    public GameObject[] enm;
    public float maxX, minX, maxY, minY;
    int st, end;

    void Start()
    {
        st = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            end = Random.Range(3, 12);
            while (true)
            {
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);
                int i = Random.Range(0, enm.Length);
                Instantiate(enm[i], new Vector2(x, y), Quaternion.identity);
                st++;
                if (st == end)
                {
                    break;
                }
            }
            Destroy(gameObject);
        }
    }

}
