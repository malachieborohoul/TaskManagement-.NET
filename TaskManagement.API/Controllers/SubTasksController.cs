using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.SubTask;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTasksController(ISubTask subTasks) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await subTasks.GetAllAsync());
        }
        
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await subTasks.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        
        [HttpGet("task/{taskId}")]

        public async Task<IActionResult> GetAllByTaskId(Guid taskId)
        {
            
           var subtasks= await subTasks.GetAllByTaskIdAsync(taskId);
           if (subtasks == null)
           {
               return NotFound("No subtasks found for the given task.");
           }

          return Ok(subtasks);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubTaskDTO model)
        {
            var response= await subTasks.CreateAsync(model);
            if (!response.Flag)
            {
                return BadRequest(response.Message);

            }

            return Created();
        }
        
           
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubTaskDTO model)
        {
            var result = await subTasks.UpdateAsync(id, model);
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
            var subtask = await subTasks.DeleteAsync(id);

            if (!subtask.Flag)
            {
                return BadRequest(subtask);
                
            }
            return Ok(subtask);
           
        }
    }
}
