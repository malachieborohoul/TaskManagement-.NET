using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

public static class Config
{

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
            new ApiScope("api1", "My API1"),

            new ApiScope("api2", "My API2")
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
                AllowedScopes = { "api1" , "api2" }
            },
            new Client
            {
                ClientId = "ResourceOwnerPasswordClient",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                // scopes that client has access to
                AllowedScopes =
                {
                    "api1" , 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                }
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