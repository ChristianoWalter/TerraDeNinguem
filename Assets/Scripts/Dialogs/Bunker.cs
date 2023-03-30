using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class Bunker : MonoBehaviour
{
    [SerializeField] Vector2 nextStartPosition;

    [SerializeField] string nextScene;

    public GameObject interactButton;

    public bool canEnter;
    public bool inDialog;
    [SerializeField] NPCConversation dialog;

    private void Start()
    {
        if (PlayerPrefs.GetString("BunkerOpen", "false") == "false")
        {
            canEnter = false;
        }
        else
        {
            canEnter = true;
        }
    }

    private void Update()
    {
        if (!interactButton) return;
        if (interactButton.activeInHierarchy && Input.GetKeyDown(KeyCode.E) && !inDialog)
        {
            UseDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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

    public void StartComents()
    {
        ConversationManager.Instance.StartConversation(dialog);


        PlayerController.Instance.canMove = false;

        inDialog = true;

    }

    private void UseDoor()
    {
        if (canEnter)
        {
            NextScene();
        }
        else
        {
            StartComents();
        }
    }

    public void EndDialog()
    {
        inDialog = false;
        PlayerController.Instance.canMove = true;
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
