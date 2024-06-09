using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.Application.Services.Role;

public interface IRoleService
{
    Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model);
    Task<IEnumerable<GetRoleDto>> GetRolesAsync();
}