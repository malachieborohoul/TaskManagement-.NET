using TaskManagement.Domain.DTOs.Request.Priority;

namespace TaskManagement.Application.Services.API.Priority;

public interface IPriorityService
{
    Task<List<Domain.Entity.Tasks.Priority>> GetAllAsync();

    Task<Domain.Entity.Tasks.Priority?> GetByIdAsync(Guid id);
    
   

    Task<Domain.Entity.Tasks.Priority> CreateAsync(CreatePriorityDTO priority);

    Task<Domain.Entity.Tasks.Priority?> UpdateAsync(Guid id, CreatePriorityDTO priority);

    Task<Domain.Entity.Tasks.Priority?> DeleteAsync(Guid id);
}