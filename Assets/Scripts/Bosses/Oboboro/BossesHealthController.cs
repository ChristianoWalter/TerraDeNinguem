using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BossesHealthController : MonoBehaviour
{
    public static BossesHealthController instance;

    [Header("Health Controller")]

    public UnityEvent endBattleAction;

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


                endBattleAction.Invoke();

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
