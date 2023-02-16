using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Fase 1")]
    public int stopBoss;
    public float stopTime;

    public GameObject trigger1;
    public GameObject trigger2;

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

    public Transform secondFaseBegin;
    public Transform firstPoint;

    [Header("Componentes")]
    public Rigidbody2D enemyRb;

    public Animator anim;

    public Transform theBoss;

    public bool canMove;

  



    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitForPoints;

        foreach (Transform pPoint in movePoints)
        {
            pPoint.SetParent(null);
        }
    }

    private void Update()
    {
        
    }


    public void SecondFase()
    {
        /*BossHealthController.instance.invencible = true;
        
        if (theBoss.transform != secondFaseBegin.transform)
        {
            MoveNPC1.instance.Move();
            if (theBoss.transform == firstPoint.transform)
            {
                ChainScript.instance.chain.breakForce = 0;
                enemyRb.freezeRotation = true;
                transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
                MoveNPC1.instance.CanMove();
            }
        }
        else if(theBoss.transform == secondFaseBegin.transform)
        {
            BossHealthController.instance.invencible = false;
            SecondMove();
        }*/

        ChainScript.instance.chain.breakForce = 0;
        enemyRb.freezeRotation = true;
        transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        SecondMove();
    }//fecha metodo

    public void FirstFase()
    {
        if (canMove)
        {
            if (Mathf.Abs(transform.position.x - moveTarget[currentPoint1].position.x) > .2)
            {
                if (transform.position.x < moveTarget[currentPoint1].position.x)
                {
                    enemyRb.velocity = new Vector2(moveSpeed1, enemyRb.velocity.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    enemyRb.velocity = new Vector2(-moveSpeed1, enemyRb.velocity.y);
                    transform.localScale = Vector3.one;
                }

                if (transform.position.y < moveTarget[currentPoint1].position.y - .5f && enemyRb.velocity.y < .1f)
                {
                    enemyRb.velocity = new Vector2(enemyRb.velocity.x, jumpForce1);
                }
            }//fecha if
            else
            {
                enemyRb.velocity = new Vector2(0f, enemyRb.velocity.y);

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
            enemyRb.velocity = Vector3.zero;
        }

        StopChainBoss();

    }

    public void SecondMove()
    {
        if (Mathf.Abs(transform.position.x - movePoints[currentPoint].position.x) > .2)
        {
            if (transform.position.x < movePoints[currentPoint].position.x)
            {
                enemyRb.velocity = new Vector2(moveSpeed, enemyRb.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                enemyRb.velocity = new Vector2(-moveSpeed, enemyRb.velocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < movePoints[currentPoint].position.y - .5f && enemyRb.velocity.y < .1f)
            {
                enemyRb.velocity = new Vector2(enemyRb.velocity.x, jumpForce);
            }
        }//fecha if
        else
        {
            enemyRb.velocity = new Vector2(0f, enemyRb.velocity.y);

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
        if (other.tag == "BossTrigger")
        {
            stopBoss++;
        }
    }

}
