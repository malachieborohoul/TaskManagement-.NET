using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Assignee;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneesController(IAssigneeService assigneeService) : ControllerBase
    {
        [HttpDelete("{taskId}/{userId}")]
        public async Task<IActionResult> DeleteAssignee(Guid taskId, string userId)
        {
            await assigneeService.DeleteAsync(taskId, userId);
            return NoContent();
        }
        
        [HttpGet("{taskId}/{userId}")]
        public async Task<IActionResult> GetAssignee(string userId, Guid taskId)
        {
            var result = await assigneeService.GetAsync( taskId, userId);
        
            if (result == null)
            {
                return NotFound();
            }
        
            return Ok(result);
        }
        [HttpGet("{taskId:guid}")]
        public async Task<IActionResult> GetAllByTaskId(Guid taskId)
        {
            
            var assignees= await assigneeService.GetAllByTaskIdAsync(taskId);
            if (assignees == null)
            {
                return NotFound("No assignees found for the given task.");
            }

            return Ok(assignees);
        }

    }
}
