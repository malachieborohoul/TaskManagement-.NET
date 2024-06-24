using IdentityModel;
using Microsoft.AspNetCore.Components.Web;
using TaskManagement.WebUI;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using NetcodeHub.Packages.Extensions.LocalStorage;
using TaskManagement.WebUI.Services.Assignee;
using TaskManagement.WebUI.Services.Auth;
using TaskManagement.WebUI.Services.Priority;
using TaskManagement.WebUI.Services.Role;
using TaskManagement.WebUI.Services.Status;
using TaskManagement.WebUI.Services.SubTask;
using TaskManagement.WebUI.Services.Tasks;
using TaskManagement.WebUI.Services.User;
using Extension= TaskManagement.WebUI.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorServer"));


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAssigneeService, AssigneeService>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<Extension.HttpClientService>();

    
builder.Services.AddAuthorizationCore();
/*
builder.Services.AddNetcodeHubLocalStorageService();
builder.Services.AddScoped<Extension. LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, Extension.CustomAuthenticationStateProvider>();
builder.Services. AddTransient<Extension.CustomHttpHandler>(); builder.Services.AddCascadingAuthenticationState();
*/

builder.Services.AddTransient<Extension.AntiforgeryHandler>();
builder.Services.AddMudServices();
        

builder.Services. AddHttpClient(Extension.Constant.HttpClientName, client =>
{
    client.BaseAddress = new Uri("https://localhost:7260/"); }).AddHttpMessageHandler<Extension.AntiforgeryHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(Extension.Constant.HttpClientName));

// Configure OIDC Authentication
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
    options.ProviderOptions.DefaultScopes.Add("api1");
    options.ProviderOptions.ResponseType = "code";
    // Configure le type de claim de rôle personnalisé
    options.UserOptions.RoleClaim = JwtClaimTypes.Role;
   
});



await builder.Build().RunAsync();