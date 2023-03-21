using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OboboroBattle : BossesHealthController
{
    [Header("Battle Controller")]
    [Header("Controle da camera da Boss Battle")]
    private CameraController cam;
    public Transform camPosition;
    public float camSpeed;

    


    // Start is called before the first frame update
    void Start()
    {
        /*cam = FindObjectOfType<CameraController>();
        cam.enabled = false;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void EndBattle()
    {

    }
}
