using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OboboroBattleDown : BossesHealthController
{
    public static OboboroBattleDown Instance;

    public GameObject allBossFight;

    [Header("Battle Controller")]
    public int attackTimes;
    private int countAttackTimes;
    public bool endBatttle;

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
        countAttackTimes = 0;

        

        projectile.bossOnGround = false;

        invencible = true;

        cam = FindObjectOfType<CameraController>();
        cam.enabled = false;
        cam.playerLimit[0].SetActive(true);
        cam.playerLimit[1].SetActive(true); 
        AudioManager.instance.PlayAboboroBoss();
    }

    // Update is called once per frame
    void Update()
    {
        if (countAttackTimes >= attackTimes)
        {
            anim.SetTrigger("stopAttack");
            
            countAttackTimes = 0;
        }

        if (!endBatttle && cam != null)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        }
        else if (cam == null)
        {
            cam = FindObjectOfType<CameraController>();
        }
    }

    public void Shooting()
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation).direction = player.position - shootPoint.position;
        bossSfx[1].Play();
        countAttackTimes++;
    }


    public void WhithoutSources()
    {
        invencible = false;
        countAttackTimes = 0;
        projectile.bossOnGround = true;
        anim.speed += 1f;
        attackTimes++;
        bossSfx[3].Play();
    }

    protected override void BossDeath()
    {
        base.BossDeath();
        cam.enabled = true;
        cam.playerLimit[0].SetActive(false);
        cam.playerLimit[1].SetActive(false);

        endBatttle = true;

        SaveGameController.Instance.SaveGame();

        PlayerPrefs.SetInt("Oboboro", 1);

        Destroy(allBossFight);

        UIController.instance.EndGame();
    }

    public void PlayLaught()
    {
        bossSfx[4].Play();
    }

}
