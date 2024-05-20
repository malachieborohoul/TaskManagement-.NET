using Application.Contracts;
using Application.DTOs.Request.Priority;
using Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Priorities")]
    [ApiController]
    public class PriorityController(IPriority priority) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await priority.GetAllAsync());
        }
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var prior = await priority.GetByIdAsync(id);

            if (prior == null)
            {
                return NotFound();
            }
            return Ok(prior);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePriorityDTO model)
        {
            return Ok(await priority.CreateAsync(model));
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreatePriorityDTO model)
        {
            return Ok(await priority.UpdateAsync(id, model));
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await priority.DeleteAsync(id);
            return Ok(model);
        }
    }
}
