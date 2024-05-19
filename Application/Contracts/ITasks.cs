using Application.DTOs.Request.Task;
using Application.DTOs.Response;
using Application.DTOs.Response.Task;
using Domain.Entity.Tasks;

namespace Application.Contracts;

public interface ITasks
{
    Task<List<GetTaskDTO>> GetAllAsync();

    Task<GetTaskDTO?> GetByIdAsync(Guid id);
    
    Task<Tasks?> GetAllByStatusIdAsync(Guid id);
    Task<Tasks?> GetAllByPriorityIdAsync(Guid id);

    Task<GeneralResponse> CreateAsync(CreateTaskDTO task);

    Task<GeneralResponse> UpdateAsync(Guid id, UpdateTaskDTO task);

    Task<GeneralResponse> DeleteAsync(Guid id);
}