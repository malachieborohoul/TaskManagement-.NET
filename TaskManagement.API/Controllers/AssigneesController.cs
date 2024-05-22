using TaskManagement.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
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
        [HttpGet("{taskId:guid}")]
        public async Task<IActionResult> GetAllByTaskId(Guid taskId)
        {
            
            var assignees= await assignee.GetAllByTaskIdAsync(taskId);
            if (assignees == null)
            {
                return NotFound("No assignees found for the given task.");
            }

            return Ok(assignees);
        }

    }
}
