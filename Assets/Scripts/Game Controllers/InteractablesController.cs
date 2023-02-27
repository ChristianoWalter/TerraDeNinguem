using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractablesController : MonoBehaviour
{
    public static InteractablesController instance;

    public GameObject interactButton;

    public UnityEvent action;

    public HealthEnemyController life;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (interactButton != null && interactButton.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            action.Invoke();
        }
        else
        {
            return;
        }

        if (life != null)
        {
            if(life.totalLife >= 0)
            {
                action.Invoke();
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactButton != null && collision.CompareTag("Player"))
        {
            interactButton.SetActive(true);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactButton != null && collision.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
        else
        {
            return;
        }
    }

    

    public void StartSkeletonBattle()
    {
        BossAttack.instance.enemyRb.simulated = true;
        BossBattle.instance.bossUI.gameObject.SetActive(true);
        BossBattle.instance.battleStarted = true;
        gameObject.SetActive(false);
    }
    public void EndSkeletonBattle()
    {
        Debug.Log("action2");

    }
    public void action3()
    {
        Debug.Log("action3");

    }

}
