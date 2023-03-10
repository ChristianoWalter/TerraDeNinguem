using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masks : Inventory
{
    public static Masks Instance;

    private void Awake()
    {
        Instance = this;
    }
}
