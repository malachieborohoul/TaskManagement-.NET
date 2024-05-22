using Application.DTOs.Request.Task;
using Application.DTOs.Response;
using Application.DTOs.Response.Task;
using Domain.Entity.Tasks;

namespace Application.Services;

public interface ITaskService
{
    Task<IEnumerable<GetTaskDTO>> GetTasksAsync();
    Task<GetTaskDTO> GetTaskAsync(Guid taskId);
    Task<GeneralResponse> CreateTaskAsync(CreateTaskDTO model);
    Task<GeneralResponse> DeleteTaskAsync(GetTaskDTO model);
    Task<GeneralResponse> UpdateTaskAsync(Guid taskId, UpdateTaskDTO model);
    Task<GeneralResponse> ChangeTaskStatusAsync(Guid taskId, ChangeTaskStatusDTO model);

}