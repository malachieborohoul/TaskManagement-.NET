using Application.DTOs.Request.Status;
using Application.DTOs.Request.Task;
using Domain.Entity.Tasks;

namespace Application.Contracts;

public interface IStatus
{
    Task<List<Status>> GetAllAsync();

    Task<Status?> GetByIdAsync(Guid id);
    
   

    Task<Status> CreateAsync(CreateStatusDTO status);

    Task<Status?> UpdateAsync(Guid id, CreateStatusDTO status);

    Task<Status?> DeleteAsync(Guid id);
}