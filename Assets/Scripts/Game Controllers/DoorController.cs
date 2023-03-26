using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    protected PlayerController thePlayer;

    [SerializeField] Vector2 nextStartPosition;

    [SerializeField] string nextScene;

    public GameObject interactButton;



    protected virtual void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

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
            interactButton.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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

        thePlayer.transform.position = nextStartPosition;

        UIController.instance.Brighting();
       
        //SaveGameController.Instance.SaveGame();

    }
}
