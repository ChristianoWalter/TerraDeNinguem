using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{

    [SerializeField] Transform platform;
    [SerializeField] Transform[] movePoints;
    [SerializeField] float smoothMovement;
    float smooth;

    Vector2 destiny;
    int index;

    private void Awake()
    {
        StayStopped(false);
    }
    private void Start()
    {
        if (movePoints.Length == 0) return;

        index = 0;
        platform.position = movePoints[index].position;

        NewDestination();

    }

    private void Update()
    {
        if(Vector2.Distance(destiny, platform.position) <= 0.3f) NewDestination();

        platform.position = Vector2.MoveTowards(platform.position, destiny, smooth * Time.deltaTime);
    }

   void NewDestination()
    {
        index++;
        if (index >= movePoints.Length) index = 0;
        destiny = movePoints[index].position;

    }

    public void StayStopped(bool _value)
    {
        if (_value)
        {
            smooth = 0;
        }
        else
        {
            smooth = smoothMovement;
        }
    }

}
