using TaskManagement.Domain.DTOs.Request.Task;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.Task;

namespace TaskManagement.Application.Contracts;

public interface ITaskRepository
{
    Task<List<GetTaskDTO>> GetAllAsync();
    Task<GetTaskDTO?> GetByIdAsync(Guid id);
    Task<Domain.Entity.Tasks.Tasks?> GetAllByStatusIdAsync(Guid id);
    Task<Domain.Entity.Tasks.Tasks?> GetAllByPriorityIdAsync(Guid id);
    Task<GeneralResponse> CreateAsync(CreateTaskDTO model);
    Task<GeneralResponse> UpdateAsync(Guid taskId, UpdateTaskDTO model);
    Task<GeneralResponse> DeleteAsync(Guid id);
    Task<GeneralResponse> ChangeTaskStatusAsync(Guid taskId, Guid statusId);
}