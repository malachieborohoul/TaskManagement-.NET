using Application.Contracts;
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
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tasks task)
        {
            return Ok(await tasks.CreateAsync(task));
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Tasks task)
        {
            return Ok(await tasks.UpdateAsync(id, task));
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await tasks.DeleteAsync(id);
            return Ok(task);
        }
    }
}
