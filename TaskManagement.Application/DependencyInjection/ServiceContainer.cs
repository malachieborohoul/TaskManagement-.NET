using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using NetcodeHub.Packages.Extensions.LocalStorage;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Services;
using TaskManagement.Application.Services.WebUI.Assignee;
using TaskManagement.Application.Services.WebUI.Auth;
using TaskManagement.Application.Services.WebUI.Priority;
using TaskManagement.Application.Services.WebUI.Role;
using TaskManagement.Application.Services.WebUI.Status;
using TaskManagement.Application.Services.WebUI.SubTask;
using TaskManagement.Application.Services.WebUI.Tasks;
using TaskManagement.Application.Services.WebUI.User;
using IAssigneeService = TaskManagement.Application.Services.WebUI.Assignee.IAssigneeService;
using IAuthService = TaskManagement.Application.Services.WebUI.Auth.IAuthService;
using IPriorityService = TaskManagement.Application.Services.WebUI.Priority.IPriorityService;
using IRoleService = TaskManagement.Application.Services.WebUI.Role.IRoleService;
using IStatusService = TaskManagement.Application.Services.WebUI.Status.IStatusService;
using ISubTaskService = TaskManagement.Application.Services.WebUI.SubTask.ISubTaskService;
using ITaskService = TaskManagement.Application.Services.WebUI.Tasks.ITaskService;
using IUserService = TaskManagement.Application.Services.WebUI.User.IUserService;

namespace TaskManagement.Application.DependencyInjection;

public static class ServiceContainer
{
   

    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
       
        
        /*
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IPriorityService, PriorityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssigneeService, AssigneeService>();
        services.AddScoped<ISubTaskService, SubTaskService>();
        services.AddScoped<IRoleService, RoleService>();
        */
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