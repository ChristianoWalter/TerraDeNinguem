using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowWalk : MonoBehaviour
{
     
    
    [SerializeField] Transform npc;
    [SerializeField] Transform[] destinations;
    int currentIndex = 0;
    public bool onVision;

    [SerializeField] Rigidbody2D rbNpc;
    [SerializeField] float speed;
    Vector2 movement;
    Vector2 destination;


    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        onVision = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex >= destinations.Length)
        {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (destinations.Length == 0) return;
        Move();
    }

    void Move()
    {
        //Vector2 _destination = destinations[currentIndex].position;
        //NPC.position = Vector2.MoveTowards (NPC.position, _destination, step * Time.deltaTime);

        Vector2 _direction = destinations[currentIndex].position - npc.position;
        _direction = _direction.normalized;
        movement = _direction * speed;
        rbNpc.velocity = movement;

        float _dist = Vector2.Distance (npc.position, destinations[currentIndex].position);

        if (Mathf.Abs (_dist) < 5f)
        {
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        currentIndex++;
        if (currentIndex >= destinations.Length)
        {
            currentIndex = 0;
        }

    }

   

    void CanMove()
    {
        destination = destinations[currentIndex].position;
        currentIndex++;
    }

    private void OnBecameInvisible()
    {
        onVision = false;
        Destroy(gameObject);        
    }
}
