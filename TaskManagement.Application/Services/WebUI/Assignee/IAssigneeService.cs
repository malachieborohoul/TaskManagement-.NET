using TaskManagement.Domain.DTOs.Response;

namespace TaskManagement.Application.Services.WebUI.Assignee;

public interface IAssigneeService
{
    Task<GeneralResponse> DeleteAssigneeAsync(Guid taskId, string userId);
    Task<GeneralResponse> GetAssigneeAsync(Guid taskId, string userId);
    
    Task<IEnumerable<Domain.Entity.Tasks.Assignee>> GetAssigneesByTaskIdAsync(Guid taskId);

}