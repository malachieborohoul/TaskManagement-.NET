using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.Entity.Authentication;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Repos;

public class RoleRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager):IRole
{
    
    private async Task<IdentityRole> FindRoleByNameAsync(string roleName) => await roleManager.FindByNameAsync(roleName);

    public async Task<IEnumerable<GetRoleDTO>> GetRolesAsync()
    {
        return (await roleManager.Roles.ToListAsync()).Adapt<IEnumerable<GetRoleDTO>>();
    }
    
    private static string CheckResponse(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(_ => _.Description);
            return string.Join(Environment.NewLine, errors);
        }

        return null!;
    }
    
    public async Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model)
    {
        try
        {
            if (await FindRoleByNameAsync(model.Name) == null)
            {
                //Create role
                var response = await roleManager.CreateAsync(new IdentityRole(model.Name));
                var error = CheckResponse(response);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);
                else
                    return new GeneralResponse(true, $"{model.Name} created");
            }
            return new GeneralResponse(false, $"{model.Name} already created");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


}