using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.Entities.Authentication;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class AuthRepository(AppDbContext context):IAuthRepository
{
    public async Task<RefreshToken> GetRefreshTokenAsync(string token)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task<RefreshToken> GetRefreshTokenByUserIdAsync(string userId)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(t => t.UserID == userId);
    }

    public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
    {
        var existingToken = await GetRefreshTokenByUserIdAsync(refreshToken.UserID);
        if (existingToken == null)
        {
            context.RefreshTokens.Add(refreshToken);
        }
        else
        {
            existingToken.Token = refreshToken.Token;
        }
        await context.SaveChangesAsync();
    }
}