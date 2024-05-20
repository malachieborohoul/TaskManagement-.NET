using Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneesController(IAssignee assignee) : ControllerBase
    {
        [HttpDelete("{taskId}/{userId}")]
        public async Task<IActionResult> DeleteAssignee(Guid taskId, string userId)
        {
            var result = await assignee.DeleteAsync(taskId, userId);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        
        // Action pour obtenir un assigné spécifique
        [HttpGet("{taskId}/{userId}")]
        public async Task<IActionResult> GetAssignee(string userId, Guid taskId)
        {
            var result = await assignee.GetAsync( taskId, userId);
        
            if (result == null)
            {
                return NotFound();
            }
        
            return Ok(result);
        }
    }
}
