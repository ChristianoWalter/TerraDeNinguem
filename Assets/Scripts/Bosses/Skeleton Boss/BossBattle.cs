using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public static BossBattle instance;

    [Header("Controle da camera da Boss Battle")]
    private CameraController cam;
    public Transform camPosition;
    public float camSpeed;

    [Header("Fases da Batalha")]
    public int treshold1;

    

    [Header("Objetos de fim da batalha")]
    public GameObject winObjects;

    private bool battleEnded;

    public string bossRef;

    public BossAttack theBoss;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
        cam.enabled = false;

        

        //AudioManager.instance.PlayBossMusic();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPosition.position, camSpeed * Time.deltaTime);


        if (!battleEnded)
        {
            if (BossHealthController.instance.currentHealth > treshold1)
            {
                theBoss.FirstFase();
            }
            else
            {
               theBoss.SecondFase();
            }

        } 
        
        else
        {
            
                if(winObjects != null)
                {
                    winObjects.SetActive(true);
                    winObjects.transform.SetParent(null);
                }

                cam.enabled = true;

                gameObject.SetActive(false);

                //AudioManager.instance.PlayLevelMusic();

                PlayerPrefs.SetInt(bossRef, 1);
            
        }
    }

    public void EndBattle()
    {
        battleEnded = true;

        //anim.SetTrigger("vanish");
        theBoss.GetComponent <Collider2D>().enabled = false;

       
    }

    

}
