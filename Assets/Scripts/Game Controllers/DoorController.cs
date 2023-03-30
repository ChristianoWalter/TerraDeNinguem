using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] Vector2 nextStartPosition;

    [SerializeField] string nextScene;

    public GameObject interactButton;

    protected virtual void Update()
    {
        if (!interactButton) return;
        if (interactButton.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            NextScene(); 
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactButton != null)
            {
                interactButton.SetActive(true);
            }
            else
            {
                NextScene();
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && interactButton != null)
        {
            interactButton.SetActive(false);
        }
    }

    protected virtual void NextScene()
    {
        StartCoroutine(UseNextSceneCo());
    }


    IEnumerator UseNextSceneCo()
    {        
        PlayerController.Instance.canMove = false;

        UIController.instance.Fading();

        yield return new WaitForSeconds(1.5f);

        if (nextScene != string.Empty)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        PlayerController.Instance.transform.position = nextStartPosition;

        UIController.instance.Brighting();

        PlayerController.Instance.canMove = true;

        RespawnController.instance.SetSpawn(nextStartPosition);
    }
}
