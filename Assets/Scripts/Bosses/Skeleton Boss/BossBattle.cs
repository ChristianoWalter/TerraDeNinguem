using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public static BossBattle instance;

    [Header("Controle da camera da Boss Battle")]
    public CameraController cam;
    public Transform camPosition;
    public float camSpeed;
    public bool camSet;
    public AudioSource[] bossSfx;

    [Header("Fases da Batalha")]
    public int treshold1;

    private bool breakChain;

    public bool secondFaseStarted;

    public bool battleStarted;

    public CapsuleCollider2D coll;

    [Header("Objetos de fim da batalha")]
    public GameObject winObjects;

    public GameObject bossUI;

    public bool battleEnded;

    public string bossRef;

    public BossAttack theBoss;

    public GameObject wall;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        battleEnded =  false;

        camSet= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (camSet)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        }

        if (battleEnded) return;


        if (BossHealthController.instance.currentHealth > treshold1 && battleStarted && !secondFaseStarted)
        {
            theBoss.FirstFase();
            //AudioManager.instance.PlaySkeletonBossMusic();
        }
        else if(BossHealthController.instance.currentHealth == treshold1 && !secondFaseStarted && !breakChain)
        {
            ChainScript.instance.chain.breakForce = 0;
            theBoss.transform.localRotation = Quaternion.Euler(Vector3.zero);
            theBoss.enemyRb.freezeRotation = true;
            secondFaseStarted = true;
            breakChain = true;
            coll.isTrigger = false;

            bossSfx[2].Play();

            theBoss.anim.SetBool("damage", true);
        }
        else if ( secondFaseStarted && breakChain)
        {
            theBoss.SecondFase();
        }
       
    }

    public void EndBattle()
    {
        battleEnded = true;

        bossSfx[3].Play();
        
        theBoss.anim.SetFloat("speed", Mathf.Abs(0f));

        bossUI.gameObject.SetActive(false);

        if (winObjects != null)
        {
            winObjects.SetActive(true);
            winObjects.transform.SetParent(null);
        }

        cam.enabled = true;

        theBoss.enemyRb.velocity = Vector3.zero;

        coll.isTrigger = true;

        theBoss.enemyRb.gravityScale = 0f;

        AudioManager.instance.PlaySuspenseMusic();

        PlayerAbilityTracker.instance.UnlockChainShield();

        PlayerPrefs.SetInt("TutorialHasDone", 1);

        PlayerPrefs.SetInt(bossRef, 1);

        UIController.instance.CutScene();
       
       //AudioManager.Instance.PlayLevelMusic();
    }


}
