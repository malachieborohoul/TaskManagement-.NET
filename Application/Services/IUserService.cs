using Application.DTOs.Response;
using Application.DTOs.Response.User;

namespace Application.Services;

public interface IUserService
{
    Task<List<GetUsersWithRolesResponseDTO>> GetUsersAsync();
}