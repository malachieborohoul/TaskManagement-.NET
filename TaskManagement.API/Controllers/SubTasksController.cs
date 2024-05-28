using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.SubTask;
using TaskManagement.Domain.DTOs.Request.SubTask;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTasksController(ISubTaskService subTasksService, ILogger<AuthController> logger, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(await subTasksService.GetAllAsync());
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
                var task = await subTasksService.GetByIdAsync(id);

                if (task == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(task);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpGet("task/{taskId}")]

        public async Task<IActionResult> GetAllByTaskId(Guid taskId)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var subTasks = await subTasksService.GetAllByTaskIdAsync(taskId);
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(subTasks);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubTaskDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var response = await subTasksService.CreateAsync(model);
                if (!response.Flag)
                {
                    logger.LogWarning("Bad request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return BadRequest(response.Message);

                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Created();
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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubTaskDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var response = await subTasksService.UpdateAsync(id, model);
                if (!response.Flag)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound(response.Message);
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(response);
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
                var subtask = await subTasksService.DeleteAsync(id);

                if (!subtask.Flag)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound(subtask);

                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(subtask);
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
