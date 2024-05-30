using TaskManagement.Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Status;
using TaskManagement.Domain.DTOs.Request.Status;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController(IStatusService statusService, ILogger<StatusController> logger, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(await statusService.GetAllAsync());
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var stat = await statusService.GetByIdAsync(id);

                if (stat == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(stat);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var status = await statusService.CreateAsync(model);
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateStatusDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var status = await statusService.UpdateAsync(id, model);
                if (status == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(status);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var status = await statusService.DeleteAsync(id);
                if (status == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(status);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
    }
}
