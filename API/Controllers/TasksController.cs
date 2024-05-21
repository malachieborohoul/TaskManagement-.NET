using Application.Contracts;
using Application.DTOs.Request.Task;
using Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasks tasks) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await tasks.GetAllAsync());
        }
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await tasks.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        
        [HttpGet("status/{statusId}")]

        public async Task<IActionResult> GetAllByStatusId(Guid statusId)
        {
            
            return Ok(await tasks.GetAllByStatusIdAsync(statusId));
        }
        
        [HttpGet("priority/{priorityId}")]

        public async Task<IActionResult> GetAllByPriorityId(Guid priorityId)
        {
            
            return Ok(await tasks.GetAllByPriorityIdAsync(priorityId));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO task)
        {
           var response= await tasks.CreateAsync(task);
           if (response.Flag)
           {
               return Created();

           }
                    return StatusCode(500, response.Message);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDTO task)
        {
            var result = await tasks.UpdateAsync(id, task);
            if (result.Flag)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await tasks.DeleteAsync(id);

            if (!task.Flag)
            {
                return BadRequest(task);
                
            }
            return Ok(task);
           
        }
        
        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> ChangeTaskStatus(Guid taskId, [FromBody] ChangeTaskStatusDTO model)
        {
          
            try
            {
                await tasks.ChangeTaskStatusAsync(taskId, model.StatusId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
