using Application.DTOs.Response;
using Application.DTOs.Response.User;

namespace Application.Contracts;

public interface IUser
{
    Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();

}