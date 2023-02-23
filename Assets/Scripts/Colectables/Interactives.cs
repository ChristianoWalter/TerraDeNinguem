using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactives : MonoBehaviour
{
    public GameObject interactButton;

    public UnityEvent action;

    private void Update()
    {
        if (interactButton.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            action.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
    }

    

    public void StartSkeletonBattle()
    {
        BossBattle.instance.bossUI.gameObject.SetActive(true);
        BossBattle.instance.battleStarted = true;
        gameObject.SetActive(false);
        Debug.Log("action1");
    }
    public void action2()
    {
        Debug.Log("action2");

    }
    public void action3()
    {
        Debug.Log("action3");

    }

}
