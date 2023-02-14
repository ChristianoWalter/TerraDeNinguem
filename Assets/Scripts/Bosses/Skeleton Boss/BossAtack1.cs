using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtack1 : MonoBehaviour
{
    [Header("Controle do Movimento")]
    public Transform[] movePoints;
    private int currentPoint;
    public float moveSpeed, waitForPoints;

    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D enemyRb;

    public Animator anim;

    public Transform theBoss;


    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitForPoints;

        foreach (Transform pPoint in movePoints)
        {
            pPoint.SetParent(null);
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
    }
}
