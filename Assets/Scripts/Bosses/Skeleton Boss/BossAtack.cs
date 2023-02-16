using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtack : MonoBehaviour
{
    [Header("Controle do Movimento")]
    public Transform[] movePoints1, movePoints2;
    private int currentPoint;
    public float moveSpeed1, moveSpeed2, waitForPoints, waitForPoints2;

    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D enemyRb;

    public Animator anim;

    public Transform theBoss;


    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitForPoints;

        foreach (Transform pPoint in movePoints1)
        {
            pPoint.SetParent(null);
        }
    }

    public void SecondFase()
    {
        if (Mathf.Abs(transform.position.x - movePoints2[currentPoint].position.x) > .2)
        {
            if (transform.position.x < movePoints2[currentPoint].position.x)
            {
                enemyRb.velocity = new Vector2(moveSpeed2, enemyRb.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                enemyRb.velocity = new Vector2(-moveSpeed2, enemyRb.velocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < movePoints2[currentPoint].position.y - .5f && enemyRb.velocity.y < .1f)
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
                waitCounter = waitForPoints2;

                currentPoint = currentPoint + 1;

                if (currentPoint >= movePoints2.Length)
                {
                    currentPoint = 0;
                }
            }
        }
    }

    public void FirstFase()
    {
        if (Mathf.Abs(transform.position.x - movePoints1[currentPoint].position.x) > .2)
        {
            if (transform.position.x < movePoints1[currentPoint].position.x)
            {
                enemyRb.velocity = new Vector2(moveSpeed1, enemyRb.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                enemyRb.velocity = new Vector2(-moveSpeed1, enemyRb.velocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < movePoints1[currentPoint].position.y - .5f && enemyRb.velocity.y < .1f)
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

                if (currentPoint >= movePoints1.Length)
                {
                    currentPoint = 0;
                }
            }
        }
    }//fecha metodo

}
