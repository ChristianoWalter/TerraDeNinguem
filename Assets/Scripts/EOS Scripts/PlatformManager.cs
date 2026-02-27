using EOSGame.Configs;
using Epic.OnlineServices;
using Epic.OnlineServices.Platform;
using System;

namespace EOSGame.PlatformManager
{

    public class PlatformManager
    {
        /** Create a public property to hold the platform instance. */
        public PlatformInterface PlatformInterface { get; private set; } = null;

        public void InitializeEOSSdk(EOSSdkConfig config)
        {
            // Assign the required initialization options.
            var initializeOptions = new InitializeOptions()
            {
                ProductName = config.GameName,
                ProductVersion = config.GameVersion
            };

            // Initialize the EOS SDK with the required options.
            Result InitEOSSdkResult = PlatformInterface.Initialize(ref initializeOptions);

            // Print the result of the EOS SDK initialization.
            if (InitEOSSdkResult == Result.Success)
            {
                Console.WriteLine("[PlatformManager::InitializeEOSSdk] EOS initialization succeeded!");
            }
            else
            {
                Console.Error.WriteLine("[PlatformManager::InitializeEOSSdk] EOS initialization" +
                    " failed with result: " + (int)InitEOSSdkResult + " - " + InitEOSSdkResult);
                return;
            }
        }

        public void CreatePlatformInstance(EOSSdkConfig config)
        {
            // Assign the required options to create your platform instance using the game's EOS SDK credentials you configured for your game in EOSSdkConfig.cs.
            var options = new Options()
            {
                ProductId = config.ProductId,
                SandboxId = config.SandboxId,
                ClientCredentials = new ClientCredentials()
                {
                    ClientId = config.ClientCredentialsId,
                    ClientSecret = config.ClientCredentialsSecret
                },
                DeploymentId = config.DeploymentId,
                EncryptionKey = config.EncryptionKey,
                //This guide provides information on how to set up a simple command-line project that demonstrates the implementation of EOS services without graphics. The EOS Overlay requires graphics. The DisableOverlay flag disables the EOS Overlay to prevent additional errors and warnings in the EOS SDK logs. If you choose to implement the EOS Overlay later, you must remove this flag.
                Flags = PlatformFlags.DisableOverlay,
                IsServer = false
            };

            // Create your platform instance with the required options.
            PlatformInterface = PlatformInterface.Create(ref options);

            // Print the result of your platform instance creation.
            if (PlatformInterface != null)
            {
                Console.WriteLine("[PlatformManager::CreatePlatformInstance]" +
                    " Platform instance created successfully!");
            }
            else
            {
                Console.Error.WriteLine("[PlatformManager::CreatePlatformInstance]" +
                    " Platform instance creation failed.");
                return;
            }
        }
    }
}
