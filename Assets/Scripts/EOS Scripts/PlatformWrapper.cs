using EOSGame.Configs;
using EOSGame.PlatformManager;
using UnityEngine;
using System;

public class PlatformWrapper : MonoBehaviour
{
    //Console.WriteLine("Hello, World!");

    EOSSdkConfig config = new EOSSdkConfig();

    PlatformManager platformManager = new PlatformManager();

    public void Awake()
    {
        platformManager.InitializeEOSSdk(config);
        // After you implement logging, register your EOS Logging callback function here.
        platformManager.CreatePlatformInstance(config);
    }

}
