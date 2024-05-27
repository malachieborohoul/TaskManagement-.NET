using TaskManagement.Domain.Entity.Authentication;

namespace TaskManagement.Application.Contracts;

public interface IAuthRepository
{
    Task<RefreshToken> GetRefreshTokenAsync(string token);
    Task<RefreshToken> GetRefreshTokenByUserIdAsync(string userId);
    Task SaveRefreshTokenAsync(RefreshToken refreshToken);
}