using Application.DTOs.Request.Account;
using Application.DTOs.Response;

namespace Application.Contracts;

public interface IAuth
{
    Task<LoginResponse> LoginAsync(LoginDTO model);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);
   
}