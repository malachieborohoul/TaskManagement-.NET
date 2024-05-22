using Application.DTOs.Request.Account;
using Application.DTOs.Response;

namespace Application.Services;

public interface IAuthService
{
    Task CreateAdmin();
    Task EnsureAuthenticatedAsync(Func<Task> onAuthenticated);
    Task<GeneralResponse> RegisterAsync(RegisterDTO model);
    Task<LoginResponse> LoginAsync(LoginDTO model);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);

    Task<IEnumerable<GetRoleDTO>> GetRolesAsync();
    Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();
    Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model);
}