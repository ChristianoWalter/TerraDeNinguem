using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [Header("Acionando a semente")]
    public float bulletSpeed;
    public Rigidbody2D rb;

    //public Transform seed;

    public Vector2 direction;

    [Header("Efeitos da semente")]
    public GameObject impactEffect;
    public GameObject seedEnemy;


    [Header("Dano da semente")]
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
        if (other.gameObject.tag == "Ground")
        {
            Instantiate(seedEnemy, rb.transform.position, Quaternion.identity);
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
