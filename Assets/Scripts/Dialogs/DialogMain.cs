using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogMain : MonoBehaviour
{
    [Header("Controle dos dialogos")]
    [SerializeField] List<NPCConversation> Dialog = new List<NPCConversation>();

    public virtual void StartDialog()
    {
        if (Dialog.Count == 0) return;

        PlayerController.Instance.canMove = false;

        ConversationManager.Instance.StartConversation(Dialog[0]);
    }

    public virtual void EndDialog()
    {
        Dialog.Remove(Dialog[0]);

        PlayerController.Instance.canMove = true;
    }
}
