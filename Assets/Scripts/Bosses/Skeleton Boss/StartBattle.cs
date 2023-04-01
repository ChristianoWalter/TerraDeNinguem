using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : InteractablesController
{
    

    public void StartSkeBattle()
    {
        BossAttack.instance.trigger1.SetActive(true);
        BossAttack.instance.trigger2.SetActive(true);
        BossAttack.instance.enemyRb.simulated = true;
        BossBattle.instance.bossUI.gameObject.SetActive(true);
        BossBattle.instance.battleStarted = true;
        BossBattle.instance.wall.gameObject.SetActive(false);
        AudioManager.instance.PlaySkeletonBossMusic();
        Destroy(gameObject);
    }
}
