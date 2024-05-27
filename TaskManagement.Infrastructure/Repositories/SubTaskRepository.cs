using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class SubTaskRepository(AppDbContext context):ISubTaskRepository
{
    public async Task<List<Domain.Entity.Tasks.SubTask>> GetAllAsync()
    {
        return await context.SubTasks.Include(s => s.Tasks).ToListAsync();
    }

    public async Task<Domain.Entity.Tasks.SubTask?> GetByIdAsync(Guid id)
    {
        return await context.SubTasks.FindAsync(id);
    }

    public async Task<List<Domain.Entity.Tasks.SubTask>> GetAllByTaskIdAsync(Guid taskId)
    {
        return await context.SubTasks
            .Where(st => st.TaskId == taskId)
            .ToListAsync();
    }

    public async Task<Domain.Entity.Tasks.SubTask> CreateAsync(Domain.Entity.Tasks.SubTask subTask)
    {
        await context.SubTasks.AddAsync(subTask);
        await context.SaveChangesAsync();
        return subTask;
    }

    public async Task<Domain.Entity.Tasks.SubTask?> UpdateAsync(Domain.Entity.Tasks.SubTask subTask)
    {
        var existingSubTask = await context.SubTasks.FindAsync(subTask.Id);
        if (existingSubTask == null)
        {
            return null;
        }

        context.Entry(existingSubTask).CurrentValues.SetValues(subTask);
        await context.SaveChangesAsync();
        return existingSubTask;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var subTask = await context.SubTasks.FindAsync(id);
        if (subTask == null)
        {
            return false;
        }

        context.SubTasks.Remove(subTask);
        await context.SaveChangesAsync();
        return true;
    }
}