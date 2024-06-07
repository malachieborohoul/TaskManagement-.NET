using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.Priority;
using TaskManagement.Domain.DTOs.Request.Priority;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.API.Controllers
{
    [Authorize(Roles = "Admin",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/Priorities")]
    [ApiController]
    public class PriorityController(IPriorityService priorityService,ILogger<PriorityController> logger, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

            try
            {
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return Ok(await priorityService.GetAllAsync());
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
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

            try
            {
                var prior = await priorityService.GetByIdAsync(id);

                if (prior == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);



                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return Ok(prior);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
            
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePriorityDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);


            try
            {
                var priority = await priorityService.CreateAsync(model);
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return CreatedAtAction(nameof(GetById), new { id = priority.Id }, priority);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreatePriorityDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);


            try
            {
                var priority = await priorityService.UpdateAsync(id, model);
                if (priority == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);
                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return Ok(priority);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var requestName = httpContextAccessor.HttpContext!.Request.Path;
            
            logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);


            try
            {
                var priority = await priorityService.DeleteAsync(id);
                if (priority == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName.Value, DateTime.UtcNow);

                return Ok(priority);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName.Value,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
    }
}
