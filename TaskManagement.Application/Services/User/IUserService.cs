using TaskManagement.Application.DTOs.Response.User;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Request.User;
using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Services;

public interface IUserService
{
    Task<List<GetUsersWithRolesResponseDTO>> GetUsersAsync();
    Task<GeneralResponse> CreateUserAsync(RegisterDTO model);
    
    Task<GeneralResponse> UpdateUserAsync(string userId, UpdateUserDTO model);
    
    Task<GeneralResponse> DeleteUserAsync(GetUsersWithRolesResponseDTO model);


}