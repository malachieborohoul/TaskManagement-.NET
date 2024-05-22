using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.Entity.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repos;

public class AssigneeRepository(AppDbContext context):IAssignee
{
    public async Task<bool> DeleteAsync(Guid taskId, string userId)
    {
        var assignee = await context.Assignees
            .FirstOrDefaultAsync(a => a.TaskId == taskId && a.UserId == userId);

        if (assignee == null)
        {
            return false;
        }

        context.Assignees.Remove(assignee);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Assignee> GetAsync(Guid taskId, string userId)
    {
        // Recherche de l'assigné correspondant dans la base de données
        return (await context.Assignees
            .FirstOrDefaultAsync(a => a.UserId == userId && a.TaskId == taskId))!;
    }
    
    public async Task<List<Assignee>> GetAllByTaskIdAsync(Guid id)
    {
        var existingTask = await context.Tasks.FindAsync(id);

        if (existingTask == null)
        {
            return null;
            
        }
        var assignees = await context.Assignees
            .Include(a => a.User)
            .Where(st => st.TaskId == id)
            .ToListAsync();

   
        return assignees;
    }
}