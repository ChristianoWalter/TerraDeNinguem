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

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!chain) 
        {
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
