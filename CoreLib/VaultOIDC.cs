using System;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;

namespace CoreLib
{
    public class VaultOIDC
    {
        private static string ClientId = "add ClientID here";
        private static string ClientSecret = "add ClientSecret here";
        private static string Auth0Domain = "<Auth0Domain>.eu.auth0.com";
        private static string Audience = "add HashiCorp URL here";

        public static string getOIDCToken()
        {
            AuthenticationApiClient auth0Client = new AuthenticationApiClient(Auth0Domain);
            // Client Credential Flow
            ClientCredentialsTokenRequest tokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                Audience = Audience,
                
            };

            try
            {
                Task<AccessTokenResponse> tokenResponse = auth0Client.GetTokenAsync(tokenRequest);
                tokenResponse.Wait();
                return tokenResponse.Result.AccessToken;
            }
            catch (Exception ex)
            {
                Log.writeError("Error with Auth0: " + ex.Message, null);
                throw ex;    
            }
        }

    } 
}