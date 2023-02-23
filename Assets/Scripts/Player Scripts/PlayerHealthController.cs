using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    [Header("Controle de vida")]
    public int currentHealth;
    public int maxHealth;

    public float invencibleLength;
    private float invencCounter;

    public float flashLength;
    private float flashCounter;

    public bool invencible;
    

    public SpriteRenderer[] playerSprites;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        invencible = false;

        //UIController.instance.UpdateSkulls(currentHealth);
    }

    // Update is called once per frame
     void Update()
    {
        Flash();
    }

    public void PlayerDamage(int damageAmount)
    {
        if (invencCounter <= 0 && !invencible)
        {

            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                gameObject.SetActive(false);

                //RespawnController.instance.Respawn();

                //AudioManager.instance.PlaySfx(8);
            }
            else
            {
                invencCounter = invencibleLength;

                //AudioManager.instance.PlaySfxAdjusted(11);
            }

            UIController.instance.UpdateSkulls(currentHealth);

        }
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateSkulls(currentHealth);
    }

    public void CuraPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateSkulls(currentHealth);
    }


    private void Flash()
    {
        if (invencCounter > 0)
        {
            invencCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            if (invencCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        }
    }

}

