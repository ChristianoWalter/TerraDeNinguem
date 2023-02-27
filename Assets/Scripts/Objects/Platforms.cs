using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public static Platforms Instance;

    public Animator anim;

    private void Awake()
    {
        Instance= this;
    }


    public void UpPlatform()
    {
        anim.SetTrigger("upPlatform");
    }

}
