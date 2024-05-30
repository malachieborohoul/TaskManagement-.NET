using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.WebUI.Services.Role;

public interface IRoleService
{
    Task<IEnumerable<GetRoleDto>> GetRolesAsync();

}