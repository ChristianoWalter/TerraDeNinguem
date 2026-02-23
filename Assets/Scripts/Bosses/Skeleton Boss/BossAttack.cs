using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public static BossAttack instance;

    [Header("Fase 1")]
    public int stopBoss;
    public float stopTime;

    public GameObject trigger1;
    public GameObject trigger2;
    public bool activeTrigger;

    public Transform[] moveTarget;
    private int currentPoint1;
    public float moveSpeed1, waitForPoints1;

    private float waitCounter1;
    
    public float jumpForce1;

    [Header("Fase 2")]
    public Transform[] movePoints;
    private int currentPoint;
    public float moveSpeed, waitForPoints;

    private float waitCounter;

    public float jumpForce;

    [Header("Componentes")]
    public Rigidbody2D enemyRb;

    public Animator anim;

    public Transform theBoss;

    public bool canMove;

    public int damageAmount = 1;

    //public CapsuleCollider2D coll;



    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitForPoints;

        foreach (Transform pPoint in movePoints)
        {
            pPoint.SetParent(null);
        }

        anim.SetBool("damage", false);
    }

    private void Update()
    {
        if(!canMove)
        {
            BossBattle.instance.bossSfx[1].Play();
        }
    }

    public void SecondFase()
    {
       
        if (Mathf.Abs(transform.position.x - movePoints[currentPoint].position.x) > .2)
        {
            if (transform.position.x < movePoints[currentPoint].position.x)
            {
                enemyRb.linearVelocity = new Vector2(moveSpeed, enemyRb.linearVelocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                enemyRb.linearVelocity = new Vector2(-moveSpeed, enemyRb.linearVelocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < movePoints[currentPoint].position.y - .5f && enemyRb.linearVelocity.y < .1f)
            {
                enemyRb.linearVelocity = new Vector2(enemyRb.linearVelocity.x, jumpForce);
            }
        }//fecha if
        else
        {
            enemyRb.linearVelocity = new Vector2(0f, enemyRb.linearVelocity.y);

            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitForPoints;

                currentPoint = currentPoint + 1;

                if (currentPoint >= movePoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }//fecha else

        anim.SetFloat("speed", Mathf.Abs(enemyRb.linearVelocity.x));

    }//fecha metodo

    public void FirstFase()
    {
        if (canMove)
        {
            if (Mathf.Abs(transform.position.x - moveTarget[currentPoint1].position.x) > .2)
            {
                if (transform.position.x < moveTarget[currentPoint1].position.x)
                {
                    enemyRb.linearVelocity = new Vector2(moveSpeed1, enemyRb.linearVelocity.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    enemyRb.linearVelocity = new Vector2(-moveSpeed1, enemyRb.linearVelocity.y);
                    transform.localScale = Vector3.one;
                }

                if (transform.position.y < moveTarget[currentPoint1].position.y - .5f && enemyRb.linearVelocity.y < .1f)
                {
                    enemyRb.linearVelocity = new Vector2(enemyRb.linearVelocity.x, jumpForce1);
                }
            }//fecha if
            else
            {
                enemyRb.linearVelocity = new Vector2(0f, enemyRb.linearVelocity.y);

                waitCounter1 -= Time.deltaTime;
                if (waitCounter1 <= 0)
                {
                    waitCounter1 = waitForPoints1;

                    currentPoint1 = currentPoint1 + 1;

                    if (currentPoint1 >= moveTarget.Length)
                    {
                        currentPoint1 = 0;
                    }
                }
            }//fecha else



        }//fecha if
        else
        {
            enemyRb.linearVelocity = Vector3.zero;
        }

        StopChainBoss();


        anim.SetFloat("speed", Mathf.Abs(enemyRb.linearVelocity.x));
    }

   public void ToothsSound()
    {
        BossBattle.instance.bossSfx[0].Play();
    }

    public void StopChainBoss()
    {
        if (stopBoss >= 4)
        {
            canMove = false;
            BossHealthController.instance.invencible = false;
            //enemyRb.simulated = false;
            trigger1.SetActive(false);
            trigger2.SetActive(false);
            stopTime -= Time.deltaTime;

            if (stopTime <= 0)
            {
                stopBoss = 0;
            }
        }
        else if (stopBoss <= 0)
        {
            canMove = true;
            BossHealthController.instance.invencible = true;
            //enemyRb.simulated = true;
            stopTime = 3f;
            trigger1.SetActive(true);
            trigger2.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //contagem de idas
        if (other.tag == "BossTrigger" && activeTrigger)
        {
            stopBoss++;
            activeTrigger = false;
        }

        if (other.gameObject.tag == "Player" && BossBattle.instance.battleEnded == false)
        {
            DealDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BossTrigger")
        {
            activeTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && BossBattle.instance.battleEnded == false)
        {
            DealDamage();
        }
    } 
      

    void DealDamage()
    {
        PlayerHealthController.instance.PlayerDamage(damageAmount);
    }
}
