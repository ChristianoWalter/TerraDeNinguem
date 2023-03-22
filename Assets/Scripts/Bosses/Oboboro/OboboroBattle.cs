using System.Collections;
using System.Collections.Generic;
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
    public bool canMove;

    [Header("Controle da camera da Boss Battle")]
    private CameraController cam;
    public Transform camPosition;
    public float camSpeed;

    [Header("Componentes do Boss")]
    public Animator anim;
    public Transform shootPoint;
    public Seed projectile;
    public MovablePlatform platform;

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
        
        if (platform != null)
        {
            platform.StayStopped(true);
        }

        if (canMove)
        {
            platform.StayStopped(false);
        }
        else
        {

            platform.StayStopped(true);
        }
        /*cam = FindObjectOfType<CameraController>();
        cam.enabled = false;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (countAttackTimes == attackTimes)
        {
            countAttackTimes = 0;
            if (!highLevel)
            {
                sumToUp++;
            }

            anim.SetTrigger("stopAttack");
        }


        if (sumToUp >= timeToUp)
        {
            UpFase();
        }

        //anim.SetBool("highLevel", highLevel);
    }

    public void Shooting()
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation).direction = player.position - shootPoint.position;
        countAttackTimes++;
    }

    public void UpFase()
    {
        canMove = true;
        if (canMove)
        {
            invencible = true;
            sumToUp = 0;
            highLevel = true;
            StemHealth.Instance.stemInvencible = false;
            canMove = false;
            //anim.SetTrigger("up");
        }
    }

    public void Down()
    {
        canMove = true;
        if (canMove)
        {
            platform.StayStopped(false);
            //anim.SetTrigger("down");
            invencible = false;
            highLevel = false;
            canMove = false;
        }
    }

    protected override void BossDeath()
    {
        base.BossDeath();

    }
}
