using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator1 : MonoBehaviour
{
    public GameObject bossToActive;

    public string bossRef;

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.HasKey(bossRef))
            {
                if (PlayerPrefs.GetInt(bossRef) != 1)
                {
                    bossToActive.SetActive(true);

                    gameObject.SetActive(false);
                }
            }
            else
            {
                bossToActive.SetActive(true);

                gameObject.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }
}
