
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.Entities.Authentication;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(o => o.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        // Configuration de l'identité
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();



        //Add cors
        services.AddCors(options =>
        {
            options.AddPolicy("WebUI",
                builder => builder
                    .WithOrigins("https://localhost:7159")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
        services.AddScoped<IPriorityRepository, PriorityRepository>();

 
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssigneeRepository, AssigneeRepository>();
        services.AddScoped<ISubTaskRepository, SubTaskRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}