using TaskManagement.Domain.DTOs.Request.Status;

namespace TaskManagement.Application.Services.Status;

public interface IStatusService
{
    Task<List<Domain.Entity.Tasks.Status>> GetAllAsync();

    Task<Domain.Entity.Tasks.Status?> GetByIdAsync(Guid id);
    
   

    Task<Domain.Entity.Tasks.Status> CreateAsync(CreateStatusDTO status);

    Task<Domain.Entity.Tasks.Status?> UpdateAsync(Guid id, CreateStatusDTO status);

    Task<Domain.Entity.Tasks.Status?> DeleteAsync(Guid id);
}