using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtack1 : MonoBehaviour
{
    [Header("Fase 2")]
    public Transform[] movePoints;
    private int currentPoint;
    public float moveSpeed, waitForPoints;

    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D enemyRb;

    public Animator anim;

    public Transform theBoss;

    public Transform secondFaseBegin;

    [Header("Fase 1")]
    public int stopBoss;
    public float stopTime;

    public GameObject trigger1;
    public GameObject trigger2;



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
        if (stopBoss >= 4)
        {
            enemyRb.simulated = false;
            trigger1.SetActive(false);
            trigger2.SetActive(false);
            stopTime -= Time.deltaTime;
            
            if(stopTime <= 0)
            {
                stopBoss = 0;
            }
        }
        else if (stopBoss <= 0)
        { 
            enemyRb.simulated = true;
            stopTime = 3f;
            trigger1.SetActive(true);
            trigger2.SetActive(true);
        }
    }


    public void SecondFase()
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
        }
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
        }
    }

    public void FirstFase()
    {
      if (BossHealthController.instance.currentHealth == 14 && theBoss.transform != secondFaseBegin.transform)
        {
            BossHealthController.instance.invencible = true;
            stopBoss = 0;
            if (transform.position != secondFaseBegin.position)
            {
                enemyRb.velocity = new Vector2(moveSpeed, moveSpeed);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        } 
        
        else if (BossHealthController.instance.currentHealth == 14 && theBoss.transform == secondFaseBegin.transform)
        {
            BossHealthController.instance.invencible = false;
            enemyRb.simulated = false;
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
