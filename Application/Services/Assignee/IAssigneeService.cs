using Application.DTOs.Response;
using Application.DTOs.Response.Task;
using Domain.Entity.Tasks;

namespace Application.Services;

public interface IAssigneeService
{
    Task<GeneralResponse> DeleteAssigneeAsync(Guid taskId, string userId);
    Task<GeneralResponse> GetAssigneeAsync(Guid taskId, string userId);
    
    Task<IEnumerable<Assignee>> GetAssigneesByTaskIdAsync(Guid taskId);

}