using TaskManagement.Domain.DTOs.Request.SubTask;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.SubTask;

namespace TaskManagement.Application.Services.SubTask;

public interface ISubTaskService
{
    Task<List<GetSubTaskDTO>> GetAllAsync();

    Task<GetSubTaskDTO?> GetByIdAsync(Guid id);
    
    Task<List<GetSubTaskDTO?>> GetAllByTaskIdAsync(Guid id);
   

    Task<GeneralResponse> CreateAsync(CreateSubTaskDTO task);

    Task<GeneralResponse> UpdateAsync(Guid id, UpdateSubTaskDTO task);

    Task<GeneralResponse> DeleteAsync(Guid id);
}