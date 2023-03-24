using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{

    private void Awake()
    {
        if (PlayerPrefs.GetString(gameObject.name, "false") == "false")
        {
            gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //aplicando dano
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.maxHealth ++;
            PlayerHealthController.instance.currentHealth ++;
            UIController.instance.ExtraLife();
            SaveGameController.Instance.SaveGame();

            int lifes = PlayerPrefs.GetInt("lifes", 0);
            lifes++;
            PlayerPrefs.SetInt("lifes", lifes);

            PlayerPrefs.SetString(gameObject.name, "true");
        }

        Destroy(gameObject);
    }

}
