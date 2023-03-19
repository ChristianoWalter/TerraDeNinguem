using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InteractablesController : MonoBehaviour
{
    public GameObject interactButton;

    public UnityEvent action;

    private void Update()
    {
        if (!interactButton) return;
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
        BossAttack.instance.enemyRb.simulated = true;
        BossBattle.instance.bossUI.gameObject.SetActive(true);
        BossBattle.instance.battleStarted = true;
        BossBattle.instance.wall.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void EndSkeletonBattle()
    {
        Destroy(gameObject);

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        Destroy(UIController.instance.gameObject);




        SceneManager.LoadScene("TestCredits");

    }

    public void action3()
    {
        Debug.Log("action3");

    }

}
