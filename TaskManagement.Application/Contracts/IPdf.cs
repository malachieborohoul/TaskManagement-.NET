using TaskManagement.Application.DTOs.Response.Task;
using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Application.Contracts;

public interface IPdf
{
    public Task<byte[]> ExportTasksToPdfAsync(List<GetTaskDTO> tasks);

    public  Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent);
}