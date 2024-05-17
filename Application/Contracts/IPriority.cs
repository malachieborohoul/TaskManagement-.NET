using Application.DTOs.Request.Priority;
using Domain.Entity.Tasks;

namespace Application.Contracts;

public interface IPriority
{
    Task<List<Priority>> GetAllAsync();

    Task<Priority?> GetByIdAsync(Guid id);
    
   

    Task<Priority> CreateAsync(CreatePriorityDTO priority);

    Task<Priority?> UpdateAsync(Guid id, CreatePriorityDTO priority);

    Task<Priority?> DeleteAsync(Guid id);
}