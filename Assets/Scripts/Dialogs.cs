using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System;

public class Dialogs : MonoBehaviour
{
    [Header("Controle dos dialogos")]
    [SerializeField] NPCConversation dialogo;
    [SerializeField] NPCConversation dialogo2;
    [SerializeField] NPCConversation dialogo3;
    [SerializeField] NPCConversation dialogo4;
    [SerializeField] NPCConversation dialogo5;
    [SerializeField] NPCConversation dialogo6;
    [SerializeField] NPCConversation dialogo7;
    [SerializeField] NPCConversation dialogo8;
    NPCConversation currentDialogo;
    public bool canTalk;

    [Header("Camera para cutscenes")]
    private CameraController cam;
    public Transform[] camPosition;
    public float camSpeed;
    public int currentCam;


    void Awake()
    {
        currentDialogo = dialogo;
        currentCam = 0;
    }

    private void Start()
    {
        if (canTalk)
        {
            InterfoneContact();
            canTalk = false;
        }
    }

    private void Update()
    {
        if (currentCam >= camPosition.Length) currentCam = 0;
    }

    private void InterfoneContact()
    {
        ConversationManager.Instance.StartConversation(currentDialogo);
        PlayerController.Instance.canMove = false;
        if(cam != null)
        {
            cam = FindObjectOfType<CameraController>();
            cam.enabled = false;

            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPosition[currentCam].position, camSpeed * Time.deltaTime);
            currentCam++;
        }
    }

    private void MoveCam()
    {
        if (cam != null)
        {
            cam = FindObjectOfType<CameraController>();
            cam.enabled = false;

            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPosition[currentCam].position, camSpeed * Time.deltaTime);
            currentCam++;
        }
    }

    private void EndConvarsation()
    {
        if (cam != null)
        {
            cam.enabled = true;
        }

        PlayerController.Instance.canMove = true;
    }
}
