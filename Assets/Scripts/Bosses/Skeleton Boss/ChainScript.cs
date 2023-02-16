using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour
{
    public static ChainScript instance;

    public LineRenderer line;

    public Transform boss;

    public BossAttack theBoss;

    public RelativeJoint2D chain;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        line.SetPosition(1, boss.position);

        if (chain.breakForce == 0)
        {
            Destroy(gameObject);
        }
    }

 
}
