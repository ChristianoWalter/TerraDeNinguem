using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceHealth : MonoBehaviour
{
    public static SourceHealth Instance;

    [SerializeField] List<GameObject> sources = new List<GameObject>();

    public float maxHealth;
    public float currentHealth;
    public GameObject destructionEffect;
    public Transform destructionPoint;
    public int currentSource;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentSource = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
 
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
                currentHealth = 0;

                SourceOut();
        }
        
    }

    public virtual void SourceOut()
    {
        if (currentSource == sources.Count -1)
        {
            currentHealth = maxHealth;
            
            OboboroBattleDown.Instance.WhithoutSources();
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, destructionPoint.position, Quaternion.identity);
            }
            OboboroBattleDown.Instance.bossSfx[2].Play();

            Destroy(gameObject);
        }
        else
        {
            OboboroBattleDown.Instance.bossSfx[2].Play();
            maxHealth += 2;
            Destroy(sources[currentSource]);
            currentHealth = maxHealth;
            currentSource++;
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, destructionPoint.position, Quaternion.identity);
            }
        }
    }
}
