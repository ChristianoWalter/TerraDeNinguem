using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public float rangeToShoot;
    private bool isShooting;

    public Transform shootPoint;

    public GameObject projectile;

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
        if (!isShooting)
        {
            if(Vector3.Distance(transform.position, player.position) < rangeToShoot)
            {
                isShooting = true;

                anim.SetBool("isShooting", isShooting);
            }
        }
        else
        {
            
            /*if (player.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);


                //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }*/
        }
    }

    public void Shooting()
    {
        GameObject tiro = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        tiro.transform.right = player.position - shootPoint.position; 
    }
}
