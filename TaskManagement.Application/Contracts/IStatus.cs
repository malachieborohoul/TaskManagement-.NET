using TaskManagement.Application.DTOs.Request.Task;
using TaskManagement.Domain.Entity.Tasks;
using TaskManagement.Application.DTOs.Request.Status;

namespace TaskManagement.Application.Contracts;

public interface IStatus
{
    Task<List<Status>> GetAllAsync();

    Task<Status?> GetByIdAsync(Guid id);
    
   

    Task<Status> CreateAsync(CreateStatusDTO status);

    Task<Status?> UpdateAsync(Guid id, CreateStatusDTO status);

    Task<Status?> DeleteAsync(Guid id);
}