using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogMain : MonoBehaviour
{
    [Header("Controle dos dialogos")]
    public List<NPCConversation> Dialog = new List<NPCConversation>();
    public int index;

    protected virtual void Start()
    {
        index = 0;
    }

    public virtual void StartDialog()
    {
        if (Dialog.Count < index) return;

        ConversationManager.Instance.StartConversation(Dialog[index]);

        PlayerController.Instance.canMove = false;

    }

    public virtual void NextDialog()
    {
        index++;

        PlayerController.Instance.canMove = true;
    }

    public virtual void EndDialog() 
    {
        PlayerController.Instance.canMove = true;
    }
}
