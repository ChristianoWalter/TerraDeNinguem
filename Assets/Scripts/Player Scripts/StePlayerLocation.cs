using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StePlayerLocation : MonoBehaviour
{


    void Start()
    {
        Vector2 newPos = Vector2.zero;
        newPos.x = PlayerPrefs.GetFloat("StartPositionX");
        newPos.y = PlayerPrefs.GetFloat("StartPositionY");

        
    }
}
