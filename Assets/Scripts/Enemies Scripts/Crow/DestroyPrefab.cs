using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefab : MonoBehaviour
{
    public CrowWalk crow;
    void Update()
    {
        if(crow.onVision == false)
        {
            Destroy(gameObject);
        }
    }
}
