using Application.Contracts;
using Application.DTOs.Request.Account;
using Application.DTOs.Response;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRole role) : ControllerBase
    {
        
   [HttpPost()]
   public async Task<ActionResult<GeneralResponse>> CreateRole(CreateRoleDTO model)
   {
       if (!ModelState.IsValid)
           return BadRequest("Model cannot be null");
       await role.CreateRoleAsync(model);
       return Created();
   }

   [HttpGet()]
   public async Task<ActionResult<IEnumerable<GetRoleDTO>>> GetRoles()
   {
       ;
       return Ok(await role.GetRolesAsync());
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
