using Duende.IdentityServer.Models;

public static class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };
    }

    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "blazor-client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RedirectUris = { "https://localhost:7159/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:7159/authentication/logout-callback" },
                AllowedScopes = { "openid", "profile", "api1" },
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}