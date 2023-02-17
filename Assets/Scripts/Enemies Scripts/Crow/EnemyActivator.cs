using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject enemyToActive;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           
            enemyToActive.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
