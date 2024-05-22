using Application.DTOs.Request.SubTask;
using Application.DTOs.Request.Task;
using Application.DTOs.Response;
using Application.DTOs.Response.SubTask;
using Application.DTOs.Response.Task;

namespace Application.Services;

public interface ISubTaskService
{
    Task<IEnumerable<GetSubTaskDTO>> GetSubTasksByTaskIdAsync(Guid taskId);
    Task<GeneralResponse> CreateSubTaskAsync(CreateSubTaskDTO model);
    Task<GeneralResponse> DeleteSubTaskAsync(GetSubTaskDTO model);
    Task<GeneralResponse> UpdateSubTaskAsync(Guid taskId, UpdateSubTaskDTO model);
}