using Application.DTOs.Request.Account;
using Application.DTOs.Response;

namespace Application.Contracts;

public interface IAuth
{
    Task CreateAdmin();
    Task<GeneralResponse> RegisterAsync(RegisterDTO model);
    Task<LoginResponse> LoginAsync(LoginDTO model);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);
    Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model);
    Task<IEnumerable<GetRoleDTO>> GetRolesAsync();
    Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();
    Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model);
}