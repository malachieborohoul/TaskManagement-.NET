using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Application.Contracts;

public interface IAssignee
{
    Task<bool> DeleteAsync(Guid taskId, string userId );
    Task<Assignee> GetAsync(Guid taskId, string userId);
    Task<List<Assignee>> GetAllByTaskIdAsync(Guid taskId);
}