using TaskManagement.Application.Contracts;

namespace TaskManagement.Application.Services.Assignee;

public class AssigneeService(IAssigneeRepository assigneeRepository, ITaskRepository taskRepository ):IAssigneeService
{
    public async Task DeleteAsync(Guid taskId, string userId)
    {
        await assigneeRepository.DeleteAsync(taskId, userId);
    }

    public async Task<Domain.Entity.Tasks.Assignee?> GetAsync(Guid taskId, string userId)
    {
        return await assigneeRepository.GetAsync(taskId, userId);
    }
    
    public async Task<List<Domain.Entity.Tasks.Assignee>?> GetAllByTaskIdAsync(Guid taskId)
    {
        var existingTask = await taskRepository.GetByIdAsync(taskId);

        if (existingTask == null)
        {
            return null;
        }

        return await assigneeRepository.GetAllByTaskIdAsync(taskId);
    }
}