using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class RoleRepository(AppDbContext context, RoleManager<IdentityRole> roleManager):IRoleRepository
{
   
    public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
    {
        return await roleManager.Roles.ToListAsync();
    }

    public async Task<IdentityRole?> FindRoleByNameAsync(string roleName)
    {
        return await roleManager.FindByNameAsync(roleName);
    }

    public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
    {
        return await roleManager.CreateAsync(role);
    }
}