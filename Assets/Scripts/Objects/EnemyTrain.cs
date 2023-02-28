using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour
{
    public int totalLife = 3;

    public GameObject deathEffect;

    public MovablePlatform platform;

    private void Start()
    {
        if(platform !=null)
        {
            platform.StayStopped(true);
        }
    }

    public void EnemyDamage(int damageAmount)
    {
        totalLife -= damageAmount;

        if (totalLife <= 0)
        {
            if (platform != null)
            {
                platform.StayStopped(false);
            }

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);

            //AudioManager.instance.PlaySfx(4);
        }

    }
}
