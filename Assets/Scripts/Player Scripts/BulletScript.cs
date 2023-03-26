using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Acionando o bullet")]
    public float bulletSpeed;
    public Rigidbody2D rb;

    public Vector2 direction;

    [Header("Efeitos do bullet")]
    public GameObject impactEffect;

    [Header("Dano do bullet")]
    public int damageAmount = 1;

    // Update is called once per frame
    void Update()
    {
        //adicionando velocidade ao tiro
        rb.velocity = direction * bulletSpeed;
    }

    //destruindo o tiro ao encostar em outro objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        //aplicando dano
        if (other.tag == "Enemy")
        {
            other.GetComponent<HealthEnemyController>().EnemyDamage(damageAmount);
        }

        //aplicando dano
        if (other.tag == "Target")
        {
            other.GetComponent<DestroyQuests>().EnemyDamage(damageAmount);
        }

        // dano ao boss
        if (other.tag == "SkeletonBoss")
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }
        
        if (other.tag == "Boss")
        {
            other.GetComponent<BossesHealthController>().TakeDamage(damageAmount);
        }
        
        if (other.tag == "Stem")
        {
            StemHealth.Instance.TakeDamage(damageAmount);
        }
        
        if (other.tag == "Source")
        {
            SourceHealth.Instance.TakeDamage(damageAmount);
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.PlayerDamage(damageAmount);
        }

        //efeito do impacto do tiro
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

  

    //destruindo o tiro ao sair da câmera
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
