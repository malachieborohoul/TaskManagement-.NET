using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResource(
                "roles",
                "Your role(s)",
                new List<string>(){ JwtClaimTypes.Role })
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("api1", "My API1"),

            new ApiScope("write", "Write"),
            new ApiScope("read", "Read")
        };
    }


    public static IEnumerable<Client> Clients()
    {
        return new List<Client>
        {
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

                },

                RedirectUris = { "https://localhost:7159/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:7159/authentication/logout-callback" },
                Enabled = true,

                AllowedCorsOrigins = { "https://localhost:7159" },
            }
        };
    }


    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "00000000-0000-0000-0000-000000000001",
                Username = "AhmedTurky",

                Password = "123456"
            }
        };
}