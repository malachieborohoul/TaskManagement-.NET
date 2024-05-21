using Application.DTOs.Response;

namespace Application.Services;

public interface IRoleService
{
    Task<IEnumerable<GetRoleDTO>> GetRolesAsync();

}