using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Request.User;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.User;
using TaskManagement.Domain.Entities.Authentication;

namespace TaskManagement.Application.Services.User;

public class UserService(IUserRepository userRepository):IUserService
{
    
    public async Task<IdentityRole> FindRoleByNameAsync(string roleName)
    {
        return await userRepository.FindRoleByNameAsync(roleName);
    }

    public async Task<ApplicationUser> FindUserByEmailAsync(string email)
    {
        return await userRepository.FindUserByEmailAsync(email);
    }

    public async Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync()
    {
        return await userRepository.GetUsersWithRolesAsync();
    }

    public async Task<GeneralResponse> RegisterAsync(RegisterDTO model)
    {
        return await userRepository.RegisterAsync(model);
    }

    public async Task CreateAdmin()
    {
        await userRepository.CreateAdmin();
    }

    public async Task<GeneralResponse> AssignUserToRole(ApplicationUser user, IdentityRole role)
    {
        return await userRepository.AssignUserToRole(user, role);
    }

    public async Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model)
    {
        return await userRepository.ChangeUserRoleAsync(model);
    }

    public async Task<GeneralResponse> UpdateAsync(string userId, UpdateUserDTO model)
    {
        return await userRepository.UpdateAsync(userId, model);
    }

    public async Task<GeneralResponse> DeleteAsync(string id)
    {
        return await userRepository.DeleteAsync(id);
    }
}