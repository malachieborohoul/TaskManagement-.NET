using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.SubTask;
using TaskManagement.Domain.DTOs.Request.SubTask;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTasksController(ISubTaskService subTasksService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await subTasksService.GetAllAsync());
        }
        
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await subTasksService.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        
        [HttpGet("task/{taskId}")]

        public async Task<IActionResult> GetAllByTaskId(Guid taskId)
        {

            var subTasks = await subTasksService.GetAllByTaskIdAsync(taskId);
            return Ok(subTasks);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubTaskDTO model)
        {
            var response= await subTasksService.CreateAsync(model);
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
            var response = await subTasksService.UpdateAsync(id, model);
            if (!response.Flag)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var subtask = await subTasksService.DeleteAsync(id);

            if (!subtask.Flag)
            {
                return NotFound(subtask);
                
            }
            return Ok(subtask);
           
        }
    }
}
