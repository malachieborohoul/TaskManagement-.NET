using Application.Contracts;
using Application.DTOs.Request.Task;
using Domain.Entity.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class TaskRepository(AppDbContext context):ITasks
{
    

    public async Task<List<Tasks>> GetAllAsync()
    {
        var tasks = await context.Tasks.ToListAsync();
       
        return tasks;
    }

    public async Task<Tasks?> GetByIdAsync(Guid id)
    {
        var existingTask = await context.Tasks.FindAsync(id);

        if (existingTask == null)
        {
            return null;
            
        }
        return await context.Tasks.FindAsync(id);

    }

    public async Task<Tasks?> GetAllByStatusIdAsync(Guid id)
    {
        var existingStatus = await context.Status.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
            
        }
       return  await context.Tasks.FirstOrDefaultAsync(task => task.Status.Id==id);
    }

    public async Task<Tasks> CreateAsync(Tasks task)
    {
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();

        return task;
    }

    public async Task<Tasks?> UpdateAsync(Guid id, Tasks task)
    {
        var existingTask = await context.Tasks.FindAsync(id);

        if (existingTask == null)
        {
            return null;
            
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;

        await context.SaveChangesAsync();
        return existingTask;
    }

    public async Task<Tasks?> DeleteAsync(Guid id)
    {
        var existingTask = await context.Tasks.FindAsync(id);

        if (existingTask == null)
        {
            return null;
            
        }
        
        context.Tasks.Remove(existingTask);
        return existingTask;
    }
}