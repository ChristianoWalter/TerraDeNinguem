using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : Inventory
{
    public static PlayerItems Instance;

    private void Awake()
    {
        Instance = this;
    }
}
