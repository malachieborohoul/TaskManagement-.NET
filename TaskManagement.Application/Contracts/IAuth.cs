using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Contracts;

public interface IAuth
{
    Task<LoginResponse> LoginAsync(LoginDTO model);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);
   
}