using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class AssigneeRepository(AppDbContext context):IAssigneeRepository
{
    public async Task DeleteAsync(Guid taskId, string userId)
    {
        var assignee = await context.Assignees
            .FirstOrDefaultAsync(a => a.TaskId == taskId && a.UserId == userId);

        if (assignee != null)
        {
            context.Assignees.Remove(assignee);
            await context.SaveChangesAsync();
        }
        
    }

    public async Task<Domain.Entity.Tasks.Assignee?> GetAsync(Guid taskId, string userId)
    {
        return await context.Assignees
            .FirstOrDefaultAsync(a => a.TaskId == taskId && a.UserId == userId);
    }

    public async Task<List<Domain.Entity.Tasks.Assignee>?> GetAllByTaskIdAsync(Guid taskId)
    {
        return await context.Assignees
            .Include(a => a.User)
            .Where(a => a.TaskId == taskId)
            .ToListAsync();
    }
}