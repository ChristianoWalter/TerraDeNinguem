using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossesHealthController : MonoBehaviour
{
    public static BossesHealthController instance;

    [Header("Health Controller")]

    public Slider bossHealth;

    public int currentHealth;

    public bool invencible;

    private void Awake()
    {
        instance = this;
    }

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

                BossDeath();

                //AudioManager.Instance.PlaySfx(0);
            }
            
            bossHealth.value = currentHealth;
        }
    }



    protected virtual void BossDeath()
    {

    }
}
