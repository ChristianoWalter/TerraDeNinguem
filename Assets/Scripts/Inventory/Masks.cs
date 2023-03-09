using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masks : Inventory
{
    public static Masks instance;

    private void Awake()
    {
        instance = this;
    }
}
