using TaskManagement.Domain.DTOs.Response.Task;

namespace TaskManagement.Application.Services.API.Pdf;

public interface IPdfService
{
    public Task<byte[]> ExportTasksToPdfAsync(List<GetTaskDTO> tasks);

    public  Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent);
}