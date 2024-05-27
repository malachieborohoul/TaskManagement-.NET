using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.User;
namespace TaskManagement.Application.Services.WebUI.Auth;

public interface IAuthService
{
    System.Threading.Tasks.Task CreateAdmin();
    Task EnsureAuthenticatedAsync(Func<Task> onAuthenticated);
    Task<GeneralResponse> RegisterAsync(RegisterDTO model);
    Task<LoginResponse> LoginAsync(LoginDTO model);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);

    Task<IEnumerable<GetRoleDto>> GetRolesAsync();
    Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();
    Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model);
}