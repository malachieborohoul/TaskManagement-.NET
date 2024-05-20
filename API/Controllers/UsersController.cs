using Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
    }
}
