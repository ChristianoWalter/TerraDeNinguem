using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC1 : MonoBehaviour
{
    public static MoveNPC1 instance;

    [SerializeField] Animator animpc;
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
        //Move();
        //Animations();
    }

    public void Move()
    {
        direction = destination - (Vector2)transform.position;
        direction = direction.normalized;

        float _dist = Vector2.Distance(transform.position, destination);

        if (Mathf.Abs(_dist) < 2f)
            direction = Vector2.zero;

        rbc.velocity = direction * speed;
    }

    void Animations()
    {
    }
  
    public void CanMove()
    {
        destination = destinations[index].position;
        index++;
    }
}
