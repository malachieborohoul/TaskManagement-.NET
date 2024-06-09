using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TaskManagement.Application.Extensions;
using TaskManagement.Domain.Entities.Authentication;

namespace TaskManagement.Infrastructure.IDbInitializer;

public class DbInitializer( UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger):IDbInitializer
{
    public void Initialize()
    {
        logger.LogInformation("Add Admin user in DB");

        if (roleManager.FindByNameAsync("Admin").Result == null)
        {
            logger.LogInformation("Admin role dosent exist create an admin role");

            roleManager.CreateAsync(new IdentityRole(Constants.Admin)).GetAwaiter().GetResult();
        }
        else
        {
            logger.LogInformation("Admin role exist already");

            return;
        }

        ApplicationUser adminUser = new ApplicationUser()
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            Name = "Admin"
        };
        logger.LogInformation("Create admin user with role");

        userManager.CreateAsync(adminUser, "Admin@123").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(adminUser, Constants.Admin).GetAwaiter().GetResult();

        
        logger.LogInformation("Add claims");

        var claims1 = userManager.AddClaimsAsync(adminUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, adminUser.Name),
            new Claim(JwtClaimTypes.Role, Constants.Admin),
        }).Result;

    }
}