using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public static Platforms instance;

    [SerializeField] Rigidbody2D rbc;
    [SerializeField] float speed;
    [SerializeField] Transform[] destinations;
    Vector2 destination;
    Vector2 direction;
    int index = 0;

    private void Start()
    {
        instance = this;
        destination = transform.position;
    }

    private void Update()
    {
        if (index >= destinations.Length)
        {
            direction = Vector3.zero;
        }

    }

    private void FixedUpdate()
    {
        if (destinations.Length == 0) return;
        Move();
    }

    void Move()
    {
            direction = destination - (Vector2)transform.position;
            direction = direction.normalized;

            float _dist = Vector2.Distance(transform.position, destination);

            if (Mathf.Abs(_dist) < 2f)
                direction = Vector2.zero;

            rbc.velocity = direction * speed;

        
    }




    public void CanMove()
    {
        destination = destinations[index].position;
        index++;
    }

}
