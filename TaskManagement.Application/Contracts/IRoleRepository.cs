using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Application.Contracts;

public interface IRoleRepository
{
    Task<IEnumerable<IdentityRole>> GetRolesAsync();
    Task<IdentityRole?> FindRoleByNameAsync(string roleName);
    Task<IdentityResult> CreateRoleAsync(IdentityRole role);
}