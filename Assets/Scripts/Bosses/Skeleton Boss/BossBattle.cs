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

    private bool breakChain;

    public bool secondFaseStarted;

    public bool battleStarted;

    [Header("Objetos de fim da batalha")]
    public GameObject winObjects;

    public GameObject bossUI;

    public bool battleEnded;

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


        if (!battleEnded && BossHealthController.instance.currentHealth > treshold1 && battleStarted && !secondFaseStarted)
        {
            theBoss.FirstFase();

        }
        else if(!battleEnded && BossHealthController.instance.currentHealth == treshold1 && !secondFaseStarted && !breakChain)
        {
            ChainScript.instance.chain.breakForce = 0;
            theBoss.transform.localRotation = Quaternion.Euler(Vector3.zero);
            theBoss.enemyRb.freezeRotation = true;
            secondFaseStarted = true;
            breakChain = true;
        }
        else if (!battleEnded && secondFaseStarted && breakChain)
        {
            theBoss.SecondFase();
        }
        else if (battleEnded)
        {
            Dead();
        }
        
        /*else
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
            
        }*/
    }

    public void EndBattle()
    {
        battleEnded = true;

        bossUI.gameObject.SetActive(false);

    }

    private void Dead()
    {
        if (winObjects != null)
        {
            winObjects.SetActive(true);
            winObjects.transform.SetParent(null);
        }

        cam.enabled = true;

        theBoss.enemyRb.velocity = Vector3.zero;

        PlayerPrefs.SetInt(bossRef, 1);

        //gameObject.SetActive(false);

        //AudioManager.instance.PlayLevelMusic();
       
    }

}
