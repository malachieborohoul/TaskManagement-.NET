namespace Application.Extensions;

public static class Constant
{
    public const string BrowserStorageKey = "x-key";
    public const string HttpClientName = "WebUIClient";
    public const string HttpClientHeaderScheme = "Bearer";
    
    

    public const string RegisterRoute = "api/auth/register";
    public const string LoginRoute ="api/auth/login";
    public const string RefreshTokenRoute  = "api/auth/refresh-token";
    public const string GetRolesRoute = "api/account/identity/role/list";
    public const string GetTasksRoute = "api/tasks";
        
    public const string CreateRoleRoute = "api/account/identity/role/create"; 
    public const string CreateAdminRoute = "setting";
   
    public const string AuthenticationType = "JwtAuth"; 
    public const string GetUserWithRolesRoute = "api/account/identity/users-with-roles";
    public const string ChangeUserRoleRoute = "api/account/identity/change-role";
    

    public static class Role
    {

        public const string Admin = "Admin";
        public const string User = "User";
        
    }
}