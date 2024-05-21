using Application.DTOs.Request.Account;
using Application.DTOs.Response;
using Application.DTOs.Response.User;

namespace Application.Services;

public interface IUserService
{
    Task<List<GetUsersWithRolesResponseDTO>> GetUsersAsync();
    Task<GeneralResponse> CreateUserAsync(RegisterDTO model);
}