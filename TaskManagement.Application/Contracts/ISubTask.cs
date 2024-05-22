using TaskManagement.Application.DTOs.Request.Task;
using TaskManagement.Application.DTOs.Request.SubTask;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.DTOs.Response.SubTask;

namespace TaskManagement.Application.Contracts;

public interface ISubTask
{
    Task<List<GetSubTaskDTO>> GetAllAsync();

    Task<GetSubTaskDTO?> GetByIdAsync(Guid id);
    
    Task<List<GetSubTaskDTO?>> GetAllByTaskIdAsync(Guid id);
   

    Task<GeneralResponse> CreateAsync(CreateSubTaskDTO task);

    Task<GeneralResponse> UpdateAsync(Guid id, UpdateSubTaskDTO task);

    Task<GeneralResponse> DeleteAsync(Guid id);
}