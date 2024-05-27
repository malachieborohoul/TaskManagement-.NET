namespace TaskManagement.Application.Contracts;

public interface IPriorityRepository
{
    Task<List<Domain.Entity.Tasks.Priority>> GetAllAsync();

    Task<Domain.Entity.Tasks.Priority?> GetByIdAsync(Guid id);
    
   

    Task<Domain.Entity.Tasks.Priority> CreateAsync(Domain.Entity.Tasks.Priority priority);

    Task<Domain.Entity.Tasks.Priority?> UpdateAsync(Domain.Entity.Tasks.Priority priority);

    Task<Domain.Entity.Tasks.Priority?> DeleteAsync(Guid id);
}