using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemyController : MonoBehaviour
{
    public int totalLife = 3;

    public GameObject deathEffect;

    public void EnemyDamage(int damageAmount)
    {
        totalLife -= damageAmount;

        if (totalLife <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);

            //AudioManager.instance.PlaySfx(4);
        }
    }
}
