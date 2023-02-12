using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        //aplicando dano
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.maxHealth ++;
            PlayerHealthController.instance.currentHealth ++;
            UIController.instance.ExtraLife();
        }

        Destroy(gameObject);
    }

}
