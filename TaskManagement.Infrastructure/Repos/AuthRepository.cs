using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.Extensions;
using TaskManagement.Domain.Entity.Authentication;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repos;

public class AuthRepository( UserManager<ApplicationUser> userManager, IConfiguration config, SignInManager<ApplicationUser> signInManager, AppDbContext context):IAuth
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
            //Check if email exists
            var user = await FindUserByEmailAsync(model.EmailAddress);
            if (user is null)
                return new LoginResponse(false, "User not found");
            SignInResult result;
            
            //If email exists

            try
            {
                result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false); 
            }
            catch (Exception e)
            {
                return new LoginResponse(false, "Invalid credentials");
            }
            
            // Password doesnt match
            if (!result.Succeeded)
                return new LoginResponse(false, "Invalid credentials");
            
            //Password match

            string jwtToken = await GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            
            // Token or Refresh token empty
            if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refreshToken))
                return new LoginResponse(false, "Error occured while logging in account, please contact administration");
            
            // If not save the refresh token in the db and return successfull message
            else
            {
                var saveResult = await SaveRefreshToken(user.Id, refreshToken);
                if (saveResult.Flag)
                    return new LoginResponse(true, $"{user.Name} successfully logged in", jwtToken, refreshToken);

                else
                    return new LoginResponse();

            }




        }
        catch (Exception e)
        {
            return new LoginResponse(false, e.Message);
        }
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model)
    {
        var token = await context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == model.Token);
        if (token is null) return new LoginResponse();
        var user = await userManager.FindByIdAsync(token.UserID);
        string newToken = await GenerateToken(user);
        string newRefreshToken = GenerateRefreshToken();

        var saveResult = await SaveRefreshToken(user.Id, newRefreshToken);

        if (saveResult.Flag)
            return new LoginResponse(true, $"{user.Name} successfully re-logged in", newToken, newRefreshToken);
        else
            return new LoginResponse();
    }

  



   
    private async Task<GeneralResponse> SaveRefreshToken(string userId, string token)
    {
        try
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(t => t.UserID == userId);
            if (user == null)
                context.RefreshTokens.Add(new RefreshToken() { UserID = userId, Token = token });
            else
                user.Token = token;
            await context.SaveChangesAsync();
            return new GeneralResponse(true, null!);
        }
        catch (Exception e)
        {
            return new GeneralResponse(false, e.Message);
        }
    }
}