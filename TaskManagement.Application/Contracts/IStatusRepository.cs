namespace TaskManagement.Application.Contracts;

public interface IStatusRepository
{
    Task<List<Domain.Entity.Tasks.Status>> GetAllAsync();

    Task<Domain.Entity.Tasks.Status?> GetByIdAsync(Guid id);
    
   

    Task<Domain.Entity.Tasks.Status> CreateAsync(Domain.Entity.Tasks.Status status);

    Task<Domain.Entity.Tasks.Status?> UpdateAsync(Guid id, Domain.Entity.Tasks.Status status);

    Task<Domain.Entity.Tasks.Status?> DeleteAsync(Guid id);
}