using TaskManagement.Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Priority;
using TaskManagement.Domain.DTOs.Request.Priority;

namespace TaskManagement.API.Controllers
{
    [Route("api/Priorities")]
    [ApiController]
    public class PriorityController(IPriorityService priorityService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await priorityService.GetAllAsync());
        }
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var prior = await priorityService.GetByIdAsync(id);

            if (prior == null)
            {
                return NotFound();
            }
            return Ok(prior);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePriorityDTO model)
        {
            
            var priority = await priorityService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = priority.Id }, priority);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreatePriorityDTO model)
        {
            var priority = await priorityService.UpdateAsync(id, model);
            if (priority == null) return NotFound();
            return Ok(priority);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var priority = await priorityService.DeleteAsync(id);
            if (priority == null) return NotFound();
            return Ok(priority);
        }
    }
}
