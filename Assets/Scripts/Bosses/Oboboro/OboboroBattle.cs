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

    [Header("Controle da camera da Boss Battle")]    
    public Transform camPosition;
    public float camSpeed;
    private CameraController cam;

    [Header("Componentes do Boss")]
    public Animator anim;
    public Transform shootPoint;
    public Seed projectile;

    private Transform player;

    private void Awake()
    {

        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
        sumToUp = 0;
        countAttackTimes = 0;
        
        

        
        cam = FindObjectOfType<CameraController>();
        cam.enabled = false;
        cam.playerLimit[0].SetActive(true);
        cam.playerLimit[1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == lifeToNextFase && !secFase)
        {
            anim.speed += 1f;
            secFase = true;
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
        countAttackTimes++;
    }

    public void UpFase()
    {
        highLevel = true;
        invencible = true;
        sumToUp = 0;
        StemHealth.Instance.stemInvencible = false;
        countAttackTimes = 0;
        anim.SetTrigger("up");        
    }

    public void Down()
    {
        anim.SetTrigger("down");
        highLevel = false;
        invencible = false;
        countAttackTimes = 0;
    }

    protected override void BossDeath()
    {
        base.BossDeath();
        cam.enabled = true;
        cam.playerLimit[0].SetActive(false);
        cam.playerLimit[1].SetActive(false);
    }

}
