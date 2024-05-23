using System.Text;
using TaskManagement.Domain.Entity.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Contracts;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repos;
using IUser = TaskManagement.Application.Contracts.IUser;

namespace TaskManagement.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(o => o.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
            }
        );
        services.AddAuthentication();
        services.AddAuthorization();
      
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

        services.AddScoped<IAuth, AuthRepository>();
        services.AddScoped<ITasks, TaskRepository>();
        services.AddScoped<IStatus, StatusRepository>();
        services.AddScoped<IPriority, PriorityRepository>();
        services.AddScoped<IUser, UserRepository>();
        services.AddScoped<IAssignee, AssigneeRepository>();
        services.AddScoped<ISubTask, SubTaskRepository>();
        services.AddScoped<IRole, RoleRepository>();
        services.AddScoped<IPdf, PdfRepository>();
        services.AddScoped<IExcel, ExcelRepository>();
        return services;
    }
}