using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidences : Inventory
{
    public static Evidences Instance;

    private void Awake()
    {
        Instance = this;
    }
}
