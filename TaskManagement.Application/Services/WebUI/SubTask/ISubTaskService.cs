using TaskManagement.Domain.DTOs.Request.SubTask;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.SubTask;

namespace TaskManagement.Application.Services.WebUI.SubTask;

public interface ISubTaskService
{
    Task<IEnumerable<GetSubTaskDTO>> GetSubTasksByTaskIdAsync(Guid taskId);
    Task<GeneralResponse> CreateSubTaskAsync(CreateSubTaskDTO model);
    Task<GeneralResponse> DeleteSubTaskAsync(GetSubTaskDTO model);
    Task<GeneralResponse> UpdateSubTaskAsync(Guid taskId, UpdateSubTaskDTO model);
}