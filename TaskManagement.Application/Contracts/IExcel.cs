using TaskManagement.Application.DTOs.Response.Task;
using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Application.Contracts;

public interface IExcel
{
    public Task<byte[]> ExportTasksToExcelAsync(List<GetTaskDTO> tasks);
}