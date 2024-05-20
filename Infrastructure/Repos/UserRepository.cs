using Application.Contracts;
using Application.DTOs.Response;
using Application.DTOs.Response.User;
using Domain.Entity.Authentication;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class UserRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext context):IUser
{
    public async Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync()
    {
        //Get all the users
        var allusers = await userManager.Users.ToListAsync();
        if (allusers is null) return null;

        var List = new List<GetUsersWithRolesResponseDTO>();

        foreach (var user in allusers)
        {
            //for each user retreive User role infos
            var getUserRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();
            var getRoleInfo =
                await roleManager.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == getUserRole.ToLower());
            
            // Add it in the DTO
            List.Add(new GetUsersWithRolesResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleId = getRoleInfo.Id,
                RoleName = getRoleInfo.Name
            });
        }

        return List;
    }

   
}