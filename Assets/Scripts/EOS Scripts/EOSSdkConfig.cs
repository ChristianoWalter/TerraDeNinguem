// EOSSdkConfig.cs

namespace EOSGame.Configs
{
    public class EOSSdkConfig
    {
        /** The product ID for your game, found on the Developer Portal. */
        public string ProductId { get; private set; } = "d3132dd656d1410a9744e00e6f352544";

        /** The client ID of the service permissions entry, found on the Developer Portal. */
        public string ClientCredentialsId { get; private set; } = "xyza7891tnjvOwaU2GGcVWee0AMoSKga";

        /** The client secret for accessing the set of permissions, found on the Developer Portal. */
        public string ClientCredentialsSecret { get; private set; } = "aC3GXaURye0l0uH5VBJw8hhihLijj5LiGLaZeugVPMY";

        /** The sandbox ID for your game, found on the Developer Portal. */
        public string SandboxId { get; private set; } = "458c7c0128a84fe89fd20229f763b2fb";

        /** The deployment ID for your game, found on the Developer Portal. */
        public string DeploymentId { get; private set; } = "1cd061799e24474487aef61738798375";

        /** A display name of your choice. EOS services use the display name to identify your game in logs and reporting. You can use any combination of alphanumeric or special characters. */
        public string GameName { get; private set; } = "Terra De Ninguem";

        /** A version name of your choice. EOS services use the version to identify your game in logs and reporting. You can use any combination of alphanumeric or special characters. */
        public string GameVersion { get; private set; } = "0.1.1";

        /** Encryption key. Required only if your game uses Player Data Storage or Title Data Storage. */
        public string EncryptionKey { get; private set; } = "YOUR_ENCRYPTION_KEY";
    }
}