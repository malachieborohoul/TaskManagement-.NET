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
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllWithRole()
        {
            
            return Ok(await userService.GetUsersWithRolesAsync());
        }
        
        [HttpPost()]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model cannot be null");
            await userService.RegisterAsync(model);
            return Created();
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDTO model)
        {
            
            if (!ModelState.IsValid)
                return BadRequest("Model cannot be null");
            var response = await userService.UpdateAsync(id, model);
            if (response.Flag)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);

            
        }
        
        [HttpPost("/setting")]
        public async Task<ActionResult> CreateAdmin()
        {
            await userService.CreateAdmin();
            return Created();
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
            var response = await userService.DeleteAsync(id);

            if (response.Flag)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);

           
        }

    }
    
  
}
