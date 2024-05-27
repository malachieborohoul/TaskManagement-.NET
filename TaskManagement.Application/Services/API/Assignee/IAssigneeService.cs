namespace TaskManagement.Application.Services.API.Assignee;

public interface IAssigneeService
{
    Task DeleteAsync(Guid taskId, string userId );
    Task<Domain.Entity.Tasks.Assignee?> GetAsync(Guid taskId, string userId);
    Task<List<Domain.Entity.Tasks.Assignee>?> GetAllByTaskIdAsync(Guid taskId);
}