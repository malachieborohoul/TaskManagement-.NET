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
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        
   [HttpPost()]
   public async Task<ActionResult<GeneralResponse>> CreateRole(CreateRoleDTO model)
   {
       if (!ModelState.IsValid)
           return BadRequest("Model cannot be null");
       await roleService.CreateRoleAsync(model);
       return Created();
   }

   [HttpGet()]
   public async Task<ActionResult<IEnumerable<GetRoleDto>>> GetRoles()
   {
       ;
       return Ok(await roleService.GetRolesAsync());
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
