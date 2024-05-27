namespace TaskManagement.Application.Contracts;

public interface ISubTaskRepository
{
    Task<List<Domain.Entity.Tasks.SubTask>> GetAllAsync();
    Task<Domain.Entity.Tasks.SubTask?> GetByIdAsync(Guid id);
    Task<List<Domain.Entity.Tasks.SubTask>> GetAllByTaskIdAsync(Guid taskId);
    Task<Domain.Entity.Tasks.SubTask> CreateAsync(Domain.Entity.Tasks.SubTask subTask);
    Task<Domain.Entity.Tasks.SubTask?> UpdateAsync(Domain.Entity.Tasks.SubTask subTask);
    Task<bool> DeleteAsync(Guid id);
}