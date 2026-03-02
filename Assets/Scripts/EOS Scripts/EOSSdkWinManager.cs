using Epic.OnlineServices.Platform;
using Epic.OnlineServices;
using System;
using UnityEngine;

public class EOSSdkWinManager : MonoBehaviour
{
    static private PlatformInterface s_eosPlatformInterface;

    public void Awake()
    {
        InitializePlatformInterface();

        CreatePlatformInterface();
    }

    public void Update()
    {
        if (GetEOSPlatformInterface() != null)
        {
            GetEOSPlatformInterface().Tick();
        }

        /*if (timeToShutDownMyGame)
        {
            ShutDownEOSSDK();
        }*/
    }

    public PlatformInterface GetEOSPlatformInterface()
    {
        #if UNITY_EDITOR
            return s_eosPlatformInterface;
        #else
                if (s_eosPlatformInterface == null)
                {
                    if (EOS_GetPlatformInterface() == IntPtr.Zero)
                    {
                            throw new Exception("The native code returned a NULL EOS Platform. The issue is likely in the GFX plugin.");
                    }
                    SetEOSPlatformInterface(new Epic.OnlineServices.Platform.PlatformInterface(EOS_GetPlatformInterface()));
                }
        #endif
    }


    private Epic.OnlineServices.Result InitializePlatformInterface()
    {
        InitializeOptions initOptions = new InitializeOptions();

        initOptions.ProductName = "Terra De Ninguem";
        initOptions.ProductVersion = "0.1.1";

        initOptions.AllocateMemoryFunction = IntPtr.Zero;
        initOptions.ReallocateMemoryFunction = IntPtr.Zero;
        initOptions.ReleaseMemoryFunction = IntPtr.Zero;

        var overrideThreadAffinity = new InitializeThreadAffinity();

        // You might need to configure these thread affinity values, depending on your application's needs.
        // Setting the values to Zero, which is the default in C#, lets you configure the priority through the EOS SDK.
        /*overrideThreadAffinity.NetworkWork = 0s;
        overrideThreadAffinity.StorageIo = 0;
        overrideThreadAffinity.WebSocketIo = 0;
        overrideThreadAffinity.P2PIo = 0;
        overrideThreadAffinity.HttpRequestIo = 0;
        overrideThreadAffinity.RTCIo = 0;*/

        //initOptions.OverrideThreadAffinity = overrideThreadAffinity;

        return PlatformInterface.Initialize(ref initOptions);
    }

    private PlatformInterface CreatePlatformInterface()
    {
        var clientCredentials = new Epic.OnlineServices.Platform.ClientCredentials
        {
            ClientId = "xyza7891tnjvOwaU2GGcVWee0AMoSKga",
            ClientSecret = "aC3GXaURye0l0uH5VBJw8hhihLijj5LiGLaZeugVPMY"
        };

        var platformOptions = new Epic.OnlineServices.Platform.WindowsOptions();
        platformOptions.CacheDirectory = Application.temporaryCachePath;
        platformOptions.IsServer = true;

        //For Title Storage and Player Data Storage
        platformOptions.EncryptionKey = "";

        platformOptions.OverrideCountryCode = null;
        platformOptions.OverrideLocaleCode = null;
        platformOptions.ProductId = "d3132dd656d1410a9744e00e6f352544";
        platformOptions.SandboxId = "458c7c0128a84fe89fd20229f763b2fb";
        platformOptions.DeploymentId = "1cd061799e24474487aef61738798375";

        platformOptions.TickBudgetInMilliseconds = 1;



        
        platformOptions.ClientCredentials = clientCredentials;

            platformOptions.Flags =
    #if UNITY_EDITOR
            PlatformFlags.LoadingInEditor;
    #else
            PlatformFlags.None;
    #endif

            return Epic.OnlineServices.Platform.PlatformInterface.Create(ref platformOptions);
    }

    private void ShutDownEOSSDK()
    {
        GetEOSPlatformInterface().Release();
    }
}
