using Application.DTOs.Request.SubTask;
using Application.DTOs.Request.Task;
using Application.DTOs.Response;
using Application.DTOs.Response.SubTask;

namespace Application.Contracts;

public interface ISubTask
{
    Task<List<GetSubTaskDTO>> GetAllAsync();

    Task<GetSubTaskDTO?> GetByIdAsync(Guid id);
    
    Task<List<GetSubTaskDTO?>> GetAllByTaskIdAsync(Guid id);
   

    Task<GeneralResponse> CreateAsync(CreateSubTaskDTO task);

    Task<GeneralResponse> UpdateAsync(Guid id, UpdateSubTaskDTO task);

    Task<GeneralResponse> DeleteAsync(Guid id);
}