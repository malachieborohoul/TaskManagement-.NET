using TaskManagement.Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Status;
using TaskManagement.Domain.DTOs.Request.Status;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController(IStatusService statusService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await statusService.GetAllAsync());
        }
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var stat = await statusService.GetByIdAsync(id);

            if (stat == null)
            {
                return NotFound();
            }
            return Ok(stat);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusDTO model)
        {
            var status = await statusService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateStatusDTO model)
        {
            var status = await statusService.UpdateAsync(id, model);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var status = await statusService.DeleteAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }
        
    }
}
