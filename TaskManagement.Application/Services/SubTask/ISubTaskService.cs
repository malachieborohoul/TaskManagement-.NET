using TaskManagement.Application.DTOs.Request.Task;
using TaskManagement.Application.DTOs.Response.Task;
using TaskManagement.Application.DTOs.Request.SubTask;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.DTOs.Response.SubTask;

namespace TaskManagement.Application.Services;

public interface ISubTaskService
{
    Task<IEnumerable<GetSubTaskDTO>> GetSubTasksByTaskIdAsync(Guid taskId);
    Task<GeneralResponse> CreateSubTaskAsync(CreateSubTaskDTO model);
    Task<GeneralResponse> DeleteSubTaskAsync(GetSubTaskDTO model);
    Task<GeneralResponse> UpdateSubTaskAsync(Guid taskId, UpdateSubTaskDTO model);
}