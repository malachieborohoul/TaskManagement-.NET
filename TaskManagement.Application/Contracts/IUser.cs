using TaskManagement.Application.DTOs.Response.User;
using TaskManagement.Domain.Entity.Authentication;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Request.User;
using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Contracts;

public interface IUser
{
    Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();
    public Task<GeneralResponse> RegisterAsync(RegisterDTO model);

    public Task CreateAdmin();
    public  Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model);

    public Task<GeneralResponse> UpdateAsync(string userId, UpdateUserDTO model);
    
    Task<GeneralResponse> DeleteAsync(string id);

}