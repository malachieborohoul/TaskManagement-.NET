using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Services;

public interface IRoleService
{
    Task<IEnumerable<GetRoleDTO>> GetRolesAsync();

}