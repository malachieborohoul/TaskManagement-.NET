using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Account;
using TaskManagement.Application.DTOs.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs.Request.User;

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
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDTO model)
        {
            
            if (!ModelState.IsValid)
                return BadRequest("Model cannot be null");
            var result = await user.UpdateAsync(id, model);
            if (result.Flag)
            {
                return Ok(result);
            }
            else
            {
                if (result.Message == null)
                {
                    return NotFound(result);
                }
                
                return StatusCode(500, new { error = "An error occurred while updating the resource" });

            }

            
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
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await user.DeleteAsync(id);

            if (result.Flag)
            {
                return Ok(result);
            }
            else
            {
                if (result.Message == null!)
                {
                    return NotFound(result);
                }
                
                return StatusCode(500, new { error = "An error occurred while updating the resource" });

            }

           
        }

    }
    
  
}
