using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectiles : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    public Vector3 direction;

    public int damageAmount;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.PlayerDamage(damageAmount);
        }

        if(impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
