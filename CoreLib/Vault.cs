using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.AuthMethods.UserPass;
using VaultSharp.V1;
using VaultSharp.V1.AuthMethods.JWT;
using VaultSharp.V1.AuthMethods;

namespace CoreLib
{
    public class Vault
    {
        public Vault()
        {
            UpdateSecret();
        }

        private string accessKey;
        private string secretKey;

        public string getAccessKey()
        {
            return accessKey;
        }
        public string getSecretKey()
        {
            return secretKey;
        }

        //HashiCorp Vault
        private string vaultURL = "https://hostname.domain:8200";
        private string secretName = "factorycredential";
        private string mountPoint = "factorysimulation";

        public void UpdateSecret()
        {
            IAuthMethodInfo authMethod = new JWTAuthMethodInfo("default", VaultOIDC.getOIDCToken());
            VaultClientSettings vaultClientSettings = new VaultClientSettings(vaultURL, authMethod);
            VaultClient vaultClient = new VaultClient(vaultClientSettings);

            //Use the KV Secret Engine
            Task<VaultSharp.V1.Commons.Secret<VaultSharp.V1.Commons.SecretData>> secrets = vaultClient.V1.Secrets.KeyValue.V2
                               .ReadSecretAsync(path: this.secretName, mountPoint: this.mountPoint);
            secrets.Wait();

            this.accessKey = Convert.ToString(secrets.Result.Data.Data["accesskey"]);
            this.secretKey = Convert.ToString(secrets.Result.Data.Data["secretkey"]);
        }

    } 
}