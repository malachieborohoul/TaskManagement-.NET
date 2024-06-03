using TaskManagement.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Role;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService roleService,ILogger<RolesController> logger, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        
   [HttpPost()]
   public async Task<ActionResult<GeneralResponse>> CreateRole(CreateRoleDTO model)
   {
       var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
       logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);


       try
       {
           if (!ModelState.IsValid)
               return BadRequest("Model cannot be null");
           await roleService.CreateRoleAsync(model);
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

   [HttpGet()]
   public async Task<ActionResult<IEnumerable<GetRoleDto>>> GetRoles()
   {
       var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
       logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

       try
       {
           logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

           return Ok(await roleService.GetRolesAsync());
       }
       catch (Exception e)
       {
           logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
               DateTime.UtcNow);
           return StatusCode(500, new GeneralResponse(false, e.Message));
       }
       
   }
   
 
      
        
        /*
        [HttpGet("identity/users-with-roles")]
        public async Task<ActionResult<IEnumerable<GetUsersWithRolesResponseDTO>>> GetUsersWithRoles()
        {
            return Ok(await auth.GetUsersWithRolesAsync());
        }

       
        */
    }
}
