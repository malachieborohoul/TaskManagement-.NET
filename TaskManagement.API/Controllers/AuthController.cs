using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Services.Auth;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.Entities.Authentication;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IConfiguration configuration) : ControllerBase
    {
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
      
        /*
        [HttpPost("login")]
        public async Task<ActionResult<GeneralResponse>> Login(LoginDTO model)
        {
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}",nameof(Login), DateTime.UtcNow);
            try
            {
                var response = await authService.LoginAsync(model);
                if (!response.Flag)
                {
                    logger.LogWarning("Login failed for user {@Email}, {@RequestName}, {@DateTimeUtc}",
                        model.EmailAddress, nameof(Login), DateTime.UtcNow);

                    return NotFound();
                }

                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", nameof(Login), DateTime.UtcNow);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", nameof(Login),
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPost("refresh-token")]
        public async Task<ActionResult<GeneralResponse>> RefreshToken(RefreshTokenDTO model)
        {
   
            return Ok(await authService.RefreshTokenAsync(model));
        }
   
    }*/
    }
}
