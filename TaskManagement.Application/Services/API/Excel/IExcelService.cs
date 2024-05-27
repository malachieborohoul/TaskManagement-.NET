using TaskManagement.Domain.DTOs.Response.Task;

namespace TaskManagement.Application.Services.API.Excel;

public interface IExcelService
{
    public Task<byte[]> ExportTasksToExcelAsync(List<GetTaskDTO> tasks);
}