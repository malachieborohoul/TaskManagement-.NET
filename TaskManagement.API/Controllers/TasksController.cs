using Mapster;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Task;
using TaskManagement.Domain.Entity.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasks tasks, IPdf pdf, IExcel excel) : ControllerBase
    {
        
        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportTasksToPdf()
        {
            var result = await tasks.GetAllAsync();
            
            var pdfBytes = await pdf.ExportTasksToPdfAsync(result);

            return File(pdfBytes, "application/pdf", "Tasks.pdf");
        }
        
        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportTasksToExcel()
        {
            var result = await tasks.GetAllAsync();

            var excelBytes = await excel.ExportTasksToExcelAsync(result);

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tasks.xlsx");
        }
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
        
        [HttpGet("priority/{priorityId}")]

        public async Task<IActionResult> GetAllByPriorityId(Guid priorityId)
        {
            
            return Ok(await tasks.GetAllByPriorityIdAsync(priorityId));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDTO task)
        {
           var response= await tasks.CreateAsync(task);
           if (response.Flag)
           {
               return Created();

           }
                    return StatusCode(500, response.Message);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDTO task)
        {
            var result = await tasks.UpdateAsync(id, task);
            if (result.Flag)
            {
                return Ok(result);
            }

            return StatusCode(500, new { error = "An error occurred while updating the user" });

        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await tasks.DeleteAsync(id);

            if (!task.Flag)
            {
                return StatusCode(500, new { error = "An error occurred while deleting the user" });
                
            }
            return Ok(task);
           
        }
        
        [HttpPatch("{taskId}/status")]
        public async Task<IActionResult> ChangeTaskStatus(Guid taskId, [FromBody] ChangeTaskStatusDTO model)
        {
          
            try
            {
                await tasks.ChangeTaskStatusAsync(taskId, model.StatusId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
