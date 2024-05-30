using Mapster;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.Application.Services.API.Role;

public class RoleService(RoleManager<IdentityRole> roleManager, IRoleRepository roleRepository):IRoleService
{
    
    private async Task<IdentityRole> FindRoleByNameAsync(string roleName) => await roleManager.FindByNameAsync(roleName);


    private static string CheckResponse(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(_ => _.Description);
            return string.Join(Environment.NewLine, errors);
        }

        return null!;
    }
    
 
    public async Task<IEnumerable<GetRoleDto>> GetRolesAsync()
    {
        var roles = await roleRepository.GetRolesAsync();
        return roles.Adapt<IEnumerable<GetRoleDto>>();
    }

    public async Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model)
    {
        try
        {
            var existingRole = await roleRepository.FindRoleByNameAsync(model.Name);
            if (existingRole == null)
            {
                var role = new IdentityRole(model.Name);
                var response = await roleRepository.CreateRoleAsync(role);
                if (!response.Succeeded)
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Description));
                    return new GeneralResponse(false, errorMessages);
                }
                return new GeneralResponse(true, $"{model.Name} created");
            }
            return new GeneralResponse(false, $"{model.Name} already exists");
        }
        catch (Exception e)
        {
            return new GeneralResponse(false, e.Message);
        }
    }

}