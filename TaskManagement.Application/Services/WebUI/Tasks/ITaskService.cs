using TaskManagement.Domain.DTOs.Request.Task;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.Task;

namespace TaskManagement.Application.Services.WebUI.Tasks;

public interface ITaskService
{
    Task<IEnumerable<GetTaskDTO>> GetTasksAsync();
    Task<GetTaskDTO> GetTaskAsync(Guid taskId);
    Task<GeneralResponse> CreateTaskAsync(CreateTaskDTO model);
    Task<GeneralResponse> DeleteTaskAsync(GetTaskDTO model);
    Task<GeneralResponse> UpdateTaskAsync(Guid taskId, UpdateTaskDTO model);
    Task<GeneralResponse> ChangeTaskStatusAsync(Guid taskId, ChangeTaskStatusDTO model);

    Task<byte[]> ExportPdf();
    Task<byte[]> ExportExcel();

}