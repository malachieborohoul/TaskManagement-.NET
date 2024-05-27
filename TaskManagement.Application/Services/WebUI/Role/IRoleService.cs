using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Services.WebUI.Role;

public interface IRoleService
{
    Task<IEnumerable<GetRoleDto>> GetRolesAsync();

}