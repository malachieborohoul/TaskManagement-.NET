using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUser user) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllWithRole()
        {
            
            return Ok(await user.GetUsersWithRolesAsync());
        }
        
        [HttpPost()]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model cannot be null");
            await user.RegisterAsync(model);
            return Created();
        }
        
        [HttpPost("/setting")]
        public async Task<ActionResult> CreateAdmin()
        {
            await user.CreateAdmin();
            return Ok();
        }
        /*
        [HttpPost("identity/change-role")]
        public async Task<ActionResult<GeneralResponse>> ChangeUserRole(ChangeUserRoleRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model cannot be null");
            return Ok(await user.ChangeUserRoleAsync(model));
        }*/

    }
    
  
}
