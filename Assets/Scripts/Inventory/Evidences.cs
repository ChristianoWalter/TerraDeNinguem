using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidences : Inventory
{
    public static Evidences instance;

    private void Awake()
    {
        instance = this;
    }
}
