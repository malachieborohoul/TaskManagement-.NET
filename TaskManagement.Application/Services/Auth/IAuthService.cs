using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.Application.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginDTO model);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);
   
}