using Application.DTOs.Response;
using Domain.Entity.Tasks;

namespace Application.Contracts;

public interface IAssignee
{
    Task<bool> DeleteAsync(Guid taskId, string userId );
    Task<Assignee> GetAsync(Guid taskId, string userId);
    Task<List<Assignee>> GetAllByTaskIdAsync(Guid taskId);
}