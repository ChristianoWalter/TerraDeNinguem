using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayerController : MonoBehaviour
{
    public void WalkPlay()
    {
        PlayerController.Instance.SoundWalk();
    }
}
