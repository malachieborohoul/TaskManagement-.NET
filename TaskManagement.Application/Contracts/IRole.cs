using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Contracts;

public interface IRole
{
    Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model);
    Task<IEnumerable<GetRoleDTO>> GetRolesAsync();
}