using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Bunker : DoorController
{
    public bool canEnter;
    [SerializeField] NPCConversation dialog;

    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetString("BunkerOpen", "false") == "false")
        {
            canEnter = false;
        }
        else
        {
            canEnter = true;
        }
    }

    public void StartDialog()
    {
        PlayerController.Instance.canMove = false;

        ConversationManager.Instance.StartConversation(dialog);
    }

    protected override void NextScene()
    {
        if (canEnter)
        {
            base.NextScene();
        }
        else
        {
            StartDialog();
        }
    }

    public void EndDialog()
    {
        PlayerController.Instance.canMove = true;
    }
}
