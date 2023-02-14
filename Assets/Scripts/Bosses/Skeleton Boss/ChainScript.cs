using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour
{
    public LineRenderer line;

    public Transform boss;

    private void Update()
    {
        line.SetPosition(1, boss.localPosition);
    }

 
}
