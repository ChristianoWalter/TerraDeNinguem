using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemHealth : MonoBehaviour
{
    public static StemHealth Instance;

    public float maxHealth;
    public float currentHealth;
    public bool stemInvencible;
    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        if (!stemInvencible)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                StemOut();

            }
        }
    }

    public void StemOut()
    {
        OboboroBattle.Instance.Down();
        stemInvencible = true;
        currentHealth = maxHealth;
    }
}
