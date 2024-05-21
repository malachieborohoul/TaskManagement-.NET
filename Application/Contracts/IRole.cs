using Application.DTOs.Request.Account;
using Application.DTOs.Response;

namespace Application.Contracts;

public interface IRole
{
    Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model);
    Task<IEnumerable<GetRoleDTO>> GetRolesAsync();
}