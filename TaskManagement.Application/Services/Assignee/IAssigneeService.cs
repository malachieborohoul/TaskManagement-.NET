using TaskManagement.Application.DTOs.Response.Task;
using TaskManagement.Domain.Entity.Tasks;
using TaskManagement.Application.DTOs.Response;

namespace TaskManagement.Application.Services;

public interface IAssigneeService
{
    Task<GeneralResponse> DeleteAssigneeAsync(Guid taskId, string userId);
    Task<GeneralResponse> GetAssigneeAsync(Guid taskId, string userId);
    
    Task<IEnumerable<Assignee>> GetAssigneesByTaskIdAsync(Guid taskId);

}