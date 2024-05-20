using Application.Contracts;
using Application.DTOs.Response;
using Domain.Entity.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

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
}