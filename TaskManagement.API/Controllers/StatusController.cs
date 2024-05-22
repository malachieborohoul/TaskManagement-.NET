using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Status;
using TaskManagement.Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController(IStatus status) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await status.GetAllAsync());
        }
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var stat = await status.GetByIdAsync(id);

            if (stat == null)
            {
                return NotFound();
            }
            return Ok(stat);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusDTO model)
        {
            return Ok(await status.CreateAsync(model));
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateStatusDTO model)
        {
            return Ok(await status.UpdateAsync(id, model));
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await status.DeleteAsync(id);
            return Ok(model);
        }
        
    }
}
