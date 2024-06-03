using System.Security.Claims;
using Identity.Data;
using Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Identity.IDbInitializer;

public class DbInitialize(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager):IDbInitializer
{
    public void Initialize()
    {
        if (roleManager.FindByNameAsync(SD.Admin).Result == null)
        {
            roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
        }
        else
        {
            return;
        }

        ApplicationUser adminUser = new ApplicationUser()
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            Name = "Admin"
        };
        userManager.CreateAsync(adminUser, "Admin@123").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

        var claims1 = userManager.AddClaimsAsync(adminUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, adminUser.Name),
            new Claim(JwtClaimTypes.Role, SD.Admin),
        }).Result;
        
        
        ApplicationUser customerUser = new ApplicationUser()
        {
            UserName = "customer@gmail.com",
            Email = "customer@gmail.com",
            EmailConfirmed = true,
            Name = "Customer"
        };
        userManager.CreateAsync(customerUser, "Customer@123").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

        var claims2 = userManager.AddClaimsAsync(customerUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, customerUser.Name),
            new Claim(JwtClaimTypes.Role, SD.Customer),
        }).Result;
    }
}