using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OboboroBattle : BossesHealthController
{
    public static OboboroBattle Instance;

    [Header("Battle Controller")]
    public int timeToUp;
    [SerializeField] int sumToUp;
    public bool highLevel;
    public int attackTimes;
    private int countAttackTimes;
    public int lifeToNextFase;
    public bool secFase;
    public bool endBattle;


    [Header("Controle da camera da Boss Battle")]    
    public Transform camPosition;
    public float camSpeed;
    private CameraController cam;

    [Header("Componentes do Boss")]
    public Animator anim;
    public Transform shootPoint;
    public Seed projectile;
    public AudioSource[] bossSfx;


    private Transform player;

    private void Awake()
    {

        Instance = this;
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = PlayerHealthController.instance.transform;
        sumToUp = 0;
        countAttackTimes = 0;

        projectile.bossOnGround = false;

        
        cam = FindObjectOfType<CameraController>();
        cam.enabled = false;
        cam.playerLimit[0].SetActive(true);
        cam.playerLimit[1].SetActive(true);


        bossSfx[3].Play();
        AudioManager.instance.PlayAboboroBoss();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == lifeToNextFase && !secFase)
        {
            anim.speed += 1f;
            bossSfx[7].Play();
            secFase = true;
        }
       
        if (!endBattle && cam != null)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        }
        else if (cam == null)
        {
            cam = FindObjectOfType<CameraController>();
        }

        if (countAttackTimes >= attackTimes)
        {
            anim.SetTrigger("stopAttack");
            if (!highLevel)
            {
                sumToUp++;
            }
            countAttackTimes = 0;
        }


        if (sumToUp >= timeToUp)
        {
            UpFase();
        }

        anim.SetBool("highLevel", highLevel);
    }

    public void Shooting()
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation).direction = player.position - shootPoint.position;
        bossSfx[5].Play();
        countAttackTimes++;
    }

    public void UpFase()
    {
        bossSfx[4].Play();
        bossSfx[0].Play();
        highLevel = true;
        invencible = true;
        sumToUp = 0;
        StemHealth.Instance.stemInvencible = false;
        countAttackTimes = 0;
        anim.SetTrigger("up");        
    }

    public void Down()
    {
        bossSfx[6].Play();
        bossSfx[1].Play();
        anim.SetTrigger("down");
        highLevel = false;
        invencible = false;
        countAttackTimes = 0;
    }

    public void PlayLaught()
    {
        bossSfx[2].Play();
    }

    protected override void BossDeath()
    {
        endBattle = true;
        bossSfx[8].Play();
        base.BossDeath();
        SaveGameController.Instance.SaveGame();
        cam.enabled = true;
        cam.playerLimit[0].SetActive(false);
        cam.playerLimit[1].SetActive(false);
        PlayerPrefs.SetInt("Oboboro", 1);
        UIController.instance.EndGame();
    }

}
