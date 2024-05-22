using TaskManagement.Domain.Entity.Tasks;
using TaskManagement.Application.DTOs.Request.Task;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.DTOs.Response.Task;

namespace TaskManagement.Application.Contracts;

public interface ITasks
{
    Task<List<GetTaskDTO>> GetAllAsync();

    Task<GetTaskDTO?> GetByIdAsync(Guid id);
    
    Task<Tasks?> GetAllByStatusIdAsync(Guid id);
    Task<Tasks?> GetAllByPriorityIdAsync(Guid id);

    Task<GeneralResponse> CreateAsync(CreateTaskDTO task);

    Task<GeneralResponse> UpdateAsync(Guid id, UpdateTaskDTO task);

    Task<GeneralResponse> DeleteAsync(Guid id);
    Task<GeneralResponse> ChangeTaskStatusAsync(Guid taskId, Guid statusId);
}