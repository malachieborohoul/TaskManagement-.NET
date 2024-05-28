using TaskManagement.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Auth;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
    {
      
        
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
   
    }
}
