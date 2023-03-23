using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System;

public class InterPhone : MonoBehaviour
{
    [Header("Controle dos dialogos")]
    [SerializeField] List<NPCConversation> Dialog = new List<NPCConversation>();

    public Animator anim;

    [Header("Camera para cutscenes")]
    public CameraController cam;
    public Transform[] camPosition;
    public float camSpeed;
    public int currentCam;
    public Camera camSize;

    void Awake()
    {
        currentCam = 0;
    }

    private void Start()
    {
        StartDialog();
    }

    private void Update()
    {
        if (currentCam >= camPosition.Length) currentCam = 0;
    }

    public virtual void StartDialog()
    {
        if (Dialog.Count == 0) return;


        PlayerController.Instance.canMove = false;

        ConversationManager.Instance.StartConversation(Dialog[0]);

        if (cam != null)
        {
            //cam = FindObjectOfType<CameraController>();

            cam.transform.position = camPosition[currentCam].position;//Vector3.MoveTowards(cam.transform.position, camPosition[currentCam].position, camSpeed * Time.deltaTime);
            cam.enabled = false;
            camSize.orthographicSize = 3;
            currentCam++;


        }

        anim.SetBool("on", true);
    }

    public void MoveCam()
    {
        if (cam != null)
        {
            cam = FindObjectOfType<CameraController>();
            cam.enabled = false;

            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPosition[currentCam].position, camSpeed * Time.deltaTime);
            currentCam++;
        }
    }

    public virtual void EndConversation()
    {
        if (cam != null)
        {
            cam.enabled = true;
        }
        camSize.orthographicSize = 9;

        Dialog.Remove(Dialog[0]);

        PlayerController.Instance.canMove = true;

        anim.SetBool("on", false);

    }
}
