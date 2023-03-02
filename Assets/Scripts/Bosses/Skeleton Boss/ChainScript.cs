using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour
{
    public static ChainScript instance;

    public LineRenderer line;

    public Transform boss;

    public Transform linePoint;

    public RelativeJoint2D chain;

    public GameObject destructionEffect;

    public Transform[] destructionPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!chain) 
        {
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, destructionPoint[0].position, Quaternion.identity);
                Instantiate(destructionEffect, destructionPoint[1].position, Quaternion.identity);
            }

            Destroy(gameObject);
            /*line.SetPosition(1, Vector3.zero);
            return;*/
        }

        linePoint.position = boss.position;

        line.SetPosition(1, linePoint.localPosition);

        /*if (chain.breakForce == 0)
        {
            Destroy(gameObject);
        }*/
    }

 
}
