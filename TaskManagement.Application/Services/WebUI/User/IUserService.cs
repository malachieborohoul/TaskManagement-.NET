using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Request.User;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.User;

namespace TaskManagement.Application.Services.WebUI.User;

public interface IUserService
{
    Task<List<GetUsersWithRolesResponseDTO>> GetUsersAsync();
    Task<GeneralResponse> CreateUserAsync(RegisterDTO model);
    
    Task<GeneralResponse> UpdateUserAsync(string userId, UpdateUserDTO model);
    
    Task<GeneralResponse> DeleteUserAsync(GetUsersWithRolesResponseDTO model);


}