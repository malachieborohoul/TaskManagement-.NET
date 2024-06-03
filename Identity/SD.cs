using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity;

public class SD
{
    public const string Admin = "admin";
    public const string Customer = "customer";
    
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };
    }
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope(name: "taskapi",   displayName: "Task Server"),
            new ApiScope(name: "read",   displayName: "Read your data."),
            new ApiScope(name: "write",  displayName: "Write your data."),
            new ApiScope(name: "delete", displayName: "Delete your data.")
        };
    }
    
    public static IEnumerable<Client> Clients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "service.client",                    
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "api1", "api2.read_only" }
            },
            new Client
            {
                ClientId = "task",                    
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { "task", 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                        
                },
                RedirectUris =           { "https://localhost:7159/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:7159/signout-callback-oidc" },
            }
        };
    }



    
}