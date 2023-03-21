using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornEnemy : HealthEnemyController
{
    public float rangeToUp;
    public bool canUp;

    private Transform player;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
         if (Vector3.Distance(transform.position, player.position) < rangeToUp)
         {
            anim.SetBool("canUp", canUp);
        }
        else
        {
            canUp = false;
        }
        
    }

    }
