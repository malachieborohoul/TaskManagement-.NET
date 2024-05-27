using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.Entity.Authentication;

namespace TaskManagement.Application.Services.API.Auth;

public class AuthService( UserManager<ApplicationUser> userManager, IConfiguration config, SignInManager<ApplicationUser> signInManager, IAuthRepository authRepository):IAuthService
{
     #region Add often used methods

        private async Task<ApplicationUser> FindUserByEmailAsync(string email) => await userManager.FindByEmailAsync(email);
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var userClaims = new[]
                {
                    new Claim("Id", user.Id!),
                    new Claim(ClaimTypes.Name, user.Name!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, (await userManager.GetRolesAsync(user)).FirstOrDefault().ToString()),
                    new Claim("Fullname", user.Name),

                };
                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: userClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials

                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {
                return null!;
            }
        }


     

        private static string CheckResponse(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(_ => _.Description);
                return string.Join(Environment.NewLine, errors);
            }

            return null!;
        }


    #endregion


 
    public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            try
            {
                // Check if email exists
                var user = await userManager.FindByEmailAsync(model.EmailAddress);
                if (user == null)
                    return new LoginResponse(false, "User not found");

                var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                // Password doesn't match
                if (!result.Succeeded)
                    return new LoginResponse(false, "Invalid credentials");

                // Password match
                var jwtToken = await GenerateToken(user);
                var refreshToken = GenerateRefreshToken();

                // Token or Refresh token empty
                if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refreshToken))
                    return new LoginResponse(false, "Error occurred while logging in account, please contact administration");

                // If not, save the refresh token in the db and return successful message
                var saveResult = await SaveRefreshToken(user.Id, refreshToken);
                if (saveResult.Flag)
                    return new LoginResponse(true, $"{user.Name} successfully logged in", jwtToken, refreshToken);

                return new LoginResponse(false, "Error saving refresh token");
            }
            catch (Exception e)
            {
                return new LoginResponse(false, e.Message);
            }
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model)
        {
            var token = await authRepository.GetRefreshTokenAsync(model.Token);
            if (token == null) return new LoginResponse(false, "Invalid refresh token");

            var user = await userManager.FindByIdAsync(token.UserID);
            var newToken = await GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            var saveResult = await SaveRefreshToken(user.Id, newRefreshToken);
            if (saveResult.Flag)
                return new LoginResponse(true, $"{user.Name} successfully re-logged in", newToken, newRefreshToken);

            return new LoginResponse(false, "Error saving new refresh token");
        }

        private async Task<GeneralResponse> SaveRefreshToken(string userId, string token)
        {
            try
            {
                var refreshToken = new RefreshToken { UserID = userId, Token = token };
                await authRepository.SaveRefreshTokenAsync(refreshToken);
                return new GeneralResponse(true, null);
            }
            catch (Exception e)
            {
                return new GeneralResponse(false, e.Message);
            }
        }
    
    
}