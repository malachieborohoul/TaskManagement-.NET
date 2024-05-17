using Application.DTOs.Request.Task;
using Domain.Entity.Tasks;

namespace Application.Contracts;

public interface ITasks
{
    Task<List<Tasks>> GetAllAsync();

    Task<Tasks?> GetByIdAsync(Guid id);
    
    Task<Tasks?> GetAllByStatusIdAsync(Guid id);

    Task<Tasks> CreateAsync(Tasks task);

    Task<Tasks?> UpdateAsync(Guid id, Tasks task);

    Task<Tasks?> DeleteAsync(Guid id);
}