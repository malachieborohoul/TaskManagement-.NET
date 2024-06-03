using TaskManagement.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.User;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Request.User;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, ILogger<UsersController> logger, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllWithRole()
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

            
            try
            {
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return Ok(await userService.GetUsersWithRolesAsync());
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

            try
            {
                await userService.RegisterAsync(model);
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return Created();
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model cannot be null");
                var response = await userService.UpdateAsync(id, model);
                if (response.Flag)
                {
                    logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                    return Ok(response);
                }
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return BadRequest(response.Message);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }

            
        }
        
        [HttpPost("/setting")]
        public async Task<ActionResult> CreateAdmin()
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

            try
            {
                await userService.CreateAdmin();
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return Created();
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        /*
        [HttpPost("identity/change-role")]
        public async Task<ActionResult<GeneralResponse>> ChangeUserRole(ChangeUserRoleRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model cannot be null");
            return Ok(await user.ChangeUserRoleAsync(model));
        }*/
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

            try
            {
                var response = await userService.DeleteAsync(id);

                if (response.Flag)
                {
                    logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                    return Ok(response);
                }
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return BadRequest(response.Message);

            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
           
        }

    }
    
  
}
