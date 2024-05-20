using System.Text;
using Application.Contracts;
using Application.Extensions;
using Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using NetcodeHub.Packages.Extensions.LocalStorage;

namespace Application.DependencyInjection;

public static class ServiceContainer
{
   

    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IPriorityService, PriorityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssigneeService, AssigneeService>();
        services.AddAuthorizationCore();
        services.AddNetcodeHubLocalStorageService();
        services.AddScoped<Extensions.LocalStorageService>();
        services.AddScoped<HttpClientService>();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services. AddTransient<CustomHttpHandler>(); services.AddCascadingAuthenticationState();
        services.AddMudServices();
        services. AddHttpClient(Constant.HttpClientName, client =>
        {
            client.BaseAddress = new Uri("https://localhost:7260/"); }).AddHttpMessageHandler<CustomHttpHandler>();
        return services;
    }
}