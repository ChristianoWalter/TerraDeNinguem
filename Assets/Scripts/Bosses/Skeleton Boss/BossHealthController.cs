using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;


    private void Awake()
    {
        instance = this;
    }


    public Slider bossHealth;

    public int currentHealth;

    public BossBattle theBoss;

    public bool invencible;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth.maxValue = currentHealth;
        bossHealth.value = currentHealth;
        invencible = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!invencible)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                theBoss.EndBattle();

                //AudioManager.Instance.PlaySfx(0);
            }
            else
            {
                //AudioManager.Instance.PlaySfx(1);
            }
            bossHealth.value = currentHealth;
        }
    }
}
