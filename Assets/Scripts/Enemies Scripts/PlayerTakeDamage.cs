using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public int damageAmount = 1;

    //inimigos e objetos que se autodestroem ao dar dano ao player
    public bool destroyToDamage;
    public GameObject destructionEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        PlayerHealthController.instance.PlayerDamage(damageAmount);

        if (destroyToDamage)
        {
            if(destructionEffect != null)
            {
                Instantiate(destructionEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
