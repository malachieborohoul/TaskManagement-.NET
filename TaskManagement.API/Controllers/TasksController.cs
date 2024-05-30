using System.Text;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Excel;
using TaskManagement.Application.Services.API.Pdf;
using TaskManagement.Application.Services.API.Tasks;
using TaskManagement.Application.Services.API.Tasks;
using TaskManagement.Domain.DTOs.Request.Task;
using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService taskService, IPdfService pdfService, IExcelService excelService, ILogger<TasksController> logger, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        
        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportTasksToPdf()
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var result = await taskService.GetAllAsync();
                // Get the path of the solution directory
                var solutionDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent
                    ?.FullName;

                // Load HTML template
                var templatePath = Path.Combine(solutionDirectory!, "TaskManagement.WebUI", "wwwroot", "templates",
                    "template.html");
                var logoPath = Path.Combine(solutionDirectory!, "TaskManagement.WebUI", "wwwroot", "logo.png");
                var htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);



                // Generate task rows
                var taskRows = new StringBuilder();
                foreach (var task in result)
                {
                    taskRows.Append(
                        $"<tr><td>{task.Title}</td><td>{task.Priority.Name}</td><td>{task.Status.Name}</td><td>{task.User.Name}</td><td>{task.CreatedAt:yyyy-MM-dd}</td><td>{task.DueDate:yyyy-MM-dd}</td></tr>");

                }

                // Replace placeholder with actual task rows
                var htmlContent = htmlTemplate.Replace("./logo.png", logoPath)
                    .Replace("{{TASKS}}", taskRows.ToString());


                // Convert to PDF
                var pdfBytes = await pdfService.ConvertHtmlToPdfAsync(htmlContent);

                return File(pdfBytes, "application/pdf", "Tasks.pdf");
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportTasksToExcel()
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var result = await taskService.GetAllAsync();

                var excelBytes = await excelService.ExportTasksToExcelAsync(result);

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Tasks.xlsx");
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(await taskService.GetAllAsync());
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
                var task = await taskService.GetByIdAsync(id);

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
        
        [HttpGet("status/{statusId}")]

        public async Task<IActionResult> GetAllByStatusId(Guid statusId)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var tasks = await taskService.GetAllByStatusIdAsync(statusId);
                if (tasks == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(tasks);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpGet("priority/{priorityId}")]

        public async Task<IActionResult> GetAllByPriorityId(Guid priorityId)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();
  
            try
            {
                var tasks = await taskService.GetAllByPriorityIdAsync(priorityId);
                if (tasks == null)
                {
                    logger.LogWarning("Not found request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return NotFound();
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(tasks);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO task)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var response = await taskService.CreateAsync(task);
                if (response.Flag)
                {
                    logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return Created();

                }
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, response.Message);
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
        public async Task<IActionResult> Update(Guid taskId, [FromBody] UpdateTaskDTO model)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var response = await taskService.UpdateAsync(taskId, model);
                if (!response.Flag)
                {
                    logger.LogWarning("Bad request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return BadRequest(response.Message);
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(response.Message);
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
                var response = await taskService.DeleteAsync(id);
                if (!response.Flag)
                {
                    logger.LogWarning("Bad request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return BadRequest(response.Message);
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(response.Message);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred during request {@RequestName}, {@DateTimeUtc}", requestName,
                    DateTime.UtcNow);
                return StatusCode(500, new GeneralResponse(false, e.Message));
            }
           
        }
        
        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> ChangeTaskStatus(Guid taskId, [FromBody] Guid statusId)
        {
            var requestName = httpContextAccessor.HttpContext!.GetEndpoint();

            try
            {
                var response = await taskService.ChangeTaskStatusAsync(taskId, statusId);
                if (!response.Flag)
                {
                    logger.LogWarning("Bad request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                    return BadRequest(response.Message);
                }
                logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

                return Ok(response.Message);
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
