using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Request.User;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.User;
using TaskManagement.Domain.Entities.Authentication;

namespace TaskManagement.Application.Services.API.User;

public interface IUserService
{
    Task<IdentityRole> FindRoleByNameAsync(string roleName);
    Task<ApplicationUser> FindUserByEmailAsync(string email);
    Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();
    Task<GeneralResponse> RegisterAsync(RegisterDTO model);
    Task CreateAdmin();
    Task<GeneralResponse> AssignUserToRole(ApplicationUser user, IdentityRole role);
    Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model);
    Task<GeneralResponse> UpdateAsync(string userId, UpdateUserDTO model);
    Task<GeneralResponse> DeleteAsync(string id);

}