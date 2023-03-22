using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [Header("Acionando a semente")]
    public float bulletSpeed;
    public Rigidbody2D rb;

    //public Transform seed;

    public Vector3 direction;

    [Header("Efeitos da semente")]
    public GameObject impactEffect;
    public GameObject[] seedEnemy;


    [Header("Dano da semente")]
    public int damageAmount = 1;

    private void Start()
    {
        direction = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        //adicionando velocidade ao tiro
        rb.velocity = -transform.right * bulletSpeed;
    }

    //destruindo o tiro ao encostar em outro objeto
    private void OnCollisionEnter2D(Collision2D other)
    {
        //aplicando dano
        if (other.gameObject.tag == "Ground")
        {
            Instantiate(seedEnemy[Random.Range(0, seedEnemy.Length - 1)], rb.transform.position, Quaternion.identity);
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
