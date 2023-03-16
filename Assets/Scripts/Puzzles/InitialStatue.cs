using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorStatue;

public class InitialStatue : InteractablesController
{
    public void Interact()
    {
        if (!Disco.Instance.CanInteract())
            Disco.Instance.StartPuzzle();
    }
}
