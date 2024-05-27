using System.Text;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.API.Excel;
using TaskManagement.Application.Services.API.Pdf;
using TaskManagement.Application.Services.API.Tasks;
using TaskManagement.Application.Services.API.Tasks;
using TaskManagement.Domain.DTOs.Request.Task;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService taskService, IPdfService pdfService, IExcelService excelService) : ControllerBase
    {
        
        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportTasksToPdf()
        {
            var result = await taskService.GetAllAsync();
            // Get the path of the solution directory
            var solutionDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            
            // Load HTML template
            var templatePath = Path.Combine(solutionDirectory!,"TaskManagement.WebUI", "wwwroot","templates","template.html");
            var logoPath = Path.Combine(solutionDirectory!,"TaskManagement.WebUI", "wwwroot","logo.png");
            var htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
            
            
            
            // Generate task rows
            var taskRows = new StringBuilder();
            foreach (var task in result)
            {
                taskRows.Append(
                    $"<tr><td>{task.Title}</td><td>{task.Priority.Name}</td><td>{task.Status.Name}</td><td>{task.User.Name}</td><td>{task.CreatedAt:yyyy-MM-dd}</td><td>{task.DueDate:yyyy-MM-dd}</td></tr>");
                
            }
            
            // Replace placeholder with actual task rows
            var htmlContent = htmlTemplate.Replace("./logo.png", logoPath).Replace("{{TASKS}}", taskRows.ToString());

            
            // Convert to PDF
            var pdfBytes = await pdfService.ConvertHtmlToPdfAsync(htmlContent);

            return File(pdfBytes, "application/pdf", "Tasks.pdf");
        }
        
        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportTasksToExcel()
        {
            var result = await taskService.GetAllAsync();

            var excelBytes = await excelService.ExportTasksToExcelAsync(result);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tasks.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await taskService.GetAllAsync());
        }
        [HttpGet()]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await taskService.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        
        [HttpGet("status/{statusId}")]

        public async Task<IActionResult> GetAllByStatusId(Guid statusId)
        {
            var tasks = await taskService.GetAllByStatusIdAsync(statusId);
            if (tasks == null) return NotFound();
            return Ok(tasks);
        }
        
        [HttpGet("priority/{priorityId}")]

        public async Task<IActionResult> GetAllByPriorityId(Guid priorityId)
        {
            
            var tasks = await taskService.GetAllByPriorityIdAsync(priorityId);
            if (tasks == null) return NotFound();
            return Ok(tasks);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO task)
        {
           var response= await taskService.CreateAsync(task);
           if (response.Flag)
           {
               return Created();

           }
                    return StatusCode(500, response.Message);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid taskId, [FromBody] UpdateTaskDTO model)
        {
            var response = await taskService.UpdateAsync(taskId, model);
            if (!response.Flag) return BadRequest(response.Message);
            return Ok(response.Message);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await taskService.DeleteAsync(id);
            if (!response.Flag) return BadRequest(response.Message);
            return Ok(response.Message);
           
        }
        
        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> ChangeTaskStatus(Guid taskId, [FromBody] Guid statusId)
        {
          
            var response = await taskService.ChangeTaskStatusAsync(taskId, statusId);
            if (!response.Flag) return BadRequest(response.Message);
            return Ok(response.Message);
        }
    }
}
