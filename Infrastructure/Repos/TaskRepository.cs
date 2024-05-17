using Application.Contracts;
using Application.DTOs.Request.Task;
using Domain.Entity.Authentication;
using Domain.Entity.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class TaskRepository(AppDbContext context,UserManager<ApplicationUser> userManager):ITasks
{
    #region Add often used methods

    private async Task<Status> FindStatusById(Guid id) =>await context.Status.FindAsync(id);
    private async Task<Priority> FindPriorityById(Guid id) =>await context.Priorities.FindAsync(id);
    private async Task<ApplicationUser> FindUserById(string id) => await userManager.FindByIdAsync(id);

    #endregion
    

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
        return existingTask;

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
    
    
    public async Task<Tasks?> GetAllByPriorityIdAsync(Guid id)
    {
        var existingStatus = await context.Priorities.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
            
        }
        return  await context.Tasks.FirstOrDefaultAsync(task => task.Priority.Id==id);
    }

    public async Task<Tasks> CreateAsync(CreateTaskDTO model)
    {
        var getStatus = await FindStatusById(model.StatusId);
        var getPriority = await FindPriorityById(model.PriorityId);
        var getUser = await FindUserById(model.UserId);
        var task = new Tasks()
        {
            Title = model.Title,
            Description = model.Description,
            DueDate = model.DueDate,
            Status = getStatus,
            Priority = getPriority,
            User = getUser
            
        };
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();

        return task;
    }

    public async Task<Tasks?> UpdateAsync(Guid id, UpdateTaskDTO model)
    {
        var existingTask = await context.Tasks.FindAsync(id);

        if (existingTask == null)
        {
            return null;
            
        }
        
        var getStatus = await FindStatusById(model.StatusId);
        var getPriority = await FindPriorityById(model.PriorityId);

        existingTask.Title = model.Title;
        existingTask.Description = model.Description;
        existingTask.DueDate = model.DueDate;
        existingTask.Priority = getPriority;
        existingTask.Status = getStatus;

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