using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossActivator : MonoBehaviour
{
    public BossAttack theBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theBoss.enemyRb.simulated = true;
            BossBattle.instance.bossUI.gameObject.SetActive(true);
            BossBattle.instance.battleStarted = true;
            gameObject.SetActive(false);
        }
    }
}
