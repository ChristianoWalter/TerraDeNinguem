using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public float rangeToShoot;
    private bool isShooting;

    public Transform shootPoint;

    public EnemiesProjectiles projectile;

    private Transform player;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isShooting = Vector3.Distance(transform.position, player.position) < rangeToShoot;

        if (Vector3.Distance(transform.position, player.position) < rangeToShoot)
        {
            if ((player.position.x > transform.position.x && transform.localScale.x > 0) || (player.position.x < transform.position.x && transform.localScale.x < 0))
            {
                Vector2 _localScale = transform.localScale;
                _localScale.x *= -1f;
                transform.localScale = _localScale;
            }
        }

        anim.SetBool("isShooting", isShooting);
    }

    public void Shooting()
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation).direction = player.position - shootPoint.position;
        /*GameObject tiro = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        tiro.transform.right = player.position - shootPoint.position; */
        
    }
}
