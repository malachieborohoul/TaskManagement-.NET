using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace DuendeServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Your roles", new List<string> { JwtClaimTypes.Role })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
            new ApiScope("api1", "My API1"),

        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1", "write", "read" }
            },
            
            new Client
            {
                ClientId = "console",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                // scopes that client has access to
                AllowedScopes =
                {
                    "api1",
                   
                    "roles",


                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                },
            },
            
            new Client
            {
                ClientId = "BlazorClient",
                ClientName = "Blazor Client",
                ClientSecrets = {
                    new Secret("E8C65E41BB0E4E519D409023CF5112F4".Sha256())
                },

                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                // scopes that client has access to
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1",
                    "roles",
                  





                },

                RedirectUris = {"http://172.105.109.209:7159/authentication/login-callback" },
               // RedirectUris = {"http://172.105.109.209:7159/authentication/login-callback","https://localhost:7159/authentication/login-callback" },
                PostLogoutRedirectUris = { "http://172.105.109.209:7159/authentication/logout-callback"  },
               // PostLogoutRedirectUris = { "http://172.105.109.209:7159/authentication/logout-callback", "https://localhost:7159/authentication/logout-callback"  },
               Enabled = true,

                AllowedCorsOrigins = {"http://172.105.109.209:7159" },
                //AllowedCorsOrigins = {"http://172.105.109.209:7159", "https://localhost:7159" },
            }
        };
}