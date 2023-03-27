using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyQuests : MonoBehaviour
{
    public int totalLife = 3;

    public GameObject deathEffect;

    public UnityEvent action;

    public MovablePlatform platform;

    protected virtual void Awake()
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


    protected virtual void Start()
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
            if (action != null)
            {
                action.Invoke();
            }

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            PlayerPrefs.SetString(gameObject.name, "true");

            Destroy(gameObject);

            //AudioManager.Instance.PlaySfx(4);
        }

    }

    public void MovePlatforms()
    {
        if (platform != null)
        {
            platform.StayStopped(false);
        }
    }

    public void OpenWay()
    {
        PlayerPrefs.SetString("RootsDestroyed", "true");
    }

}
