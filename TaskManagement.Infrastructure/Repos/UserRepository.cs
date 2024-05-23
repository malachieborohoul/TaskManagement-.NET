using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.DTOs.Response.User;
using TaskManagement.Application.Extensions;
using TaskManagement.Domain.Entity.Authentication;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Request.User;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.Extensions;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repos;

public class UserRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext context, IRole roleRepo):IUser
{
    
    private async Task<IdentityRole> FindRoleByNameAsync(string roleName) => await roleManager.FindByNameAsync(roleName);
    private async Task<ApplicationUser> FindUserByEmailAsync(string email) => await userManager.FindByEmailAsync(email);

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
    
    private static string CheckResponse(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(_ => _.Description);
            return string.Join(Environment.NewLine, errors);
        }

        return null!;
    }
    
    public async Task<GeneralResponse> RegisterAsync(RegisterDTO model)
    {
        try
        {
            if (await FindUserByEmailAsync(model.EmailAddress) != null)
                return null;
            var user = new ApplicationUser()
            {
                Name = model.Name,
                UserName = model.EmailAddress,
                Email = model.EmailAddress,
                PasswordHash = model.Password
            };
            var result = await userManager.CreateAsync(user, model.Password);
            string error = CheckResponse(result);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(false, error);
            var (flag, message) = await AssignUserToRole(user, new IdentityRole() { Name = model.Role });

            return new GeneralResponse(flag, message);
        }
        catch (Exception e)
        {
            return new GeneralResponse(false, e.Message); 
        }
    }
    
    
    public async Task CreateAdmin()
    {
        try
        {
            if (await FindRoleByNameAsync(Constant.Role.Admin) != null) return;
            var admin = new RegisterDTO()
            {
                Name = "Admin",
                Password = "Admin@123",
                EmailAddress = "admin@admin.com",
                Role = Constant.Role.Admin
            };
            await RegisterAsync(admin);
        }
        catch 
        {

        }
    }
    
    private async Task<GeneralResponse> AssignUserToRole(ApplicationUser user, IdentityRole role)
    {
        if (user is null || role is null) return new GeneralResponse(false, "Model state cannot be empty");
        if (await FindRoleByNameAsync(role.Name) == null)
            //Mapter to map
            await roleRepo.CreateRoleAsync(role.Adapt(new CreateRoleDTO()));
        IdentityResult result = await userManager.AddToRoleAsync(user, role.Name);
        string error = CheckResponse(result);
        if (!string.IsNullOrEmpty(error))
            return new GeneralResponse(false, error);
        else
            return new GeneralResponse(true, $"{user.Name} assigned to {role.Name} role");
    }
    
    public async Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model)
    {
        // Verify if role and email exist
        if (await FindRoleByNameAsync(model.RoleName) is null) return new GeneralResponse(false, "Role not found");
        if (await FindUserByEmailAsync(model.UserEmail) is null) return new GeneralResponse(false, "User not found");
        
        var user = await FindUserByEmailAsync(model.UserEmail);
        var previousRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();
        // Remove previous role of the user
        var removeOldRole = await userManager.RemoveFromRoleAsync(user, previousRole);
        //Check error
        var error = CheckResponse(removeOldRole);
        if (!string.IsNullOrEmpty(error))
            return new GeneralResponse(false, error);
        
        //No error add assign role to user
        var result = await userManager.AddToRoleAsync(user, model.RoleName);
        //Check response
        var response = CheckResponse(result);
        if (!string.IsNullOrEmpty(response))
            return new GeneralResponse(false, response);
        else
            return new GeneralResponse(true, "Role Changed");


    }

    public async Task<GeneralResponse> UpdateAsync(string userId, UpdateUserDTO model)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var userEntity = await context.Users.FindAsync(userId);
            if (userEntity == null)
            {
                return new GeneralResponse(false, Message:null!);
            }

            var changeRole = new ChangeUserRoleRequestDTO(userEntity.Email,model.Role)
            {
                RoleName = model.Role,
                UserEmail = userEntity.Email
            };



           var result= await ChangeUserRoleAsync(changeRole);

           if (result.Flag)
           {
               userEntity = model.Adapt(userEntity);
               context.Users.Update(userEntity);
               await context.SaveChangesAsync();

           }

           

      
       


            await transaction.CommitAsync();

            return new GeneralResponse(true, "User updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Log the exception
            return new GeneralResponse(false, "An error occurred while updating the task: " + ex.Message);
        }
    }

    public async Task<GeneralResponse> DeleteAsync(string id)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var existingUser = await context.Users.FindAsync(id);
            var previousRole = (await userManager.GetRolesAsync(existingUser)).FirstOrDefault();
            if (existingUser == null)
            {
                return new GeneralResponse(false, null!);
            }
            
            await userManager.RemoveFromRoleAsync(existingUser, previousRole);


            context.Users.Remove(existingUser);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new GeneralResponse(true, "User deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Log the exception
            return new GeneralResponse(false, "An error occurred while deleting the task: " + ex.Message);
        }
    }
}