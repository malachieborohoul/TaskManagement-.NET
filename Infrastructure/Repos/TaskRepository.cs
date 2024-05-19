using Application.Contracts;
using Application.DTOs.Request.Task;
using Application.DTOs.Response;
using Application.DTOs.Response.Task;
using Application.DTOs.Response.User;
using Domain.Entity.Authentication;
using Domain.Entity.Tasks;
using Infrastructure.Data;
using Mapster;
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
    

    public async Task<List<GetTaskDTO>> GetAllAsync()
    {
        var tasks = (await context.Tasks.Include(s=>s.Status).Include(p=>p.Priority).Include(t=>t.User ).ToListAsync());
        return tasks    .Select(task=> new GetTaskDTO()
        {
            Id=task.Id,
           Title= task.Title,
           Description=task.Description,
          CreatedAt = task.CreatedAt,
          DueDate = task.DueDate,
           Status = task.Status,
           Priority = task.Priority,
            User=new GetUserDTO()
            {
                Id = task.User.Id,
                Name = task.User.Name,
                Email = task.User.Email,
            }
           
        }).ToList();
    }

    public async Task<GetTaskDTO?> GetByIdAsync(Guid id)
    {
        var existingTask = (await context.Tasks.Include(s=>s.Status).Include(p=>p.Priority).Include(u=>u.User).FirstOrDefaultAsync(x=>x.Id==id));

        if (existingTask == null)
        {
            return null;
            
        }

        return new GetTaskDTO()
        {
            Id = existingTask.Id,
            Title = existingTask.Title,
            Description = existingTask.Description,
            CreatedAt = existingTask.CreatedAt,
            DueDate = existingTask.DueDate,
            Status = existingTask.Status,
            Priority = existingTask.Priority,
            User = new GetUserDTO()
            {
                Id = existingTask.User.Id,
                Name = existingTask.User.Name,
                Email = existingTask.User.Email,
            }

        };

    }

    public async Task<Tasks?> GetAllByStatusIdAsync(Guid id)
    {
        var existingStatus = await context.Status.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
            
        }
       return  (await context.Tasks.Include(s=>s.Status).Include(p=>p.Priority).Include(u=>u.User).FirstOrDefaultAsync(x=>x.Status.Id==id));;
    }
    
    
    public async Task<Tasks?> GetAllByPriorityIdAsync(Guid id)
    {
        var existingStatus = await context.Priorities.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
            
        }
        return  (await context.Tasks.Include(s=>s.Status).Include(p=>p.Priority).Include(u=>u.User).FirstOrDefaultAsync(x=>x.Priority.Id==id));;

    }

    public async Task<GeneralResponse> CreateAsync(CreateTaskDTO model)
    {
    
        var task= context.Tasks.Add(model.Adapt(new Tasks()));
       await context.SaveChangesAsync();

        return new GeneralResponse(true, "Task saved successfully");
    }

    public async Task<GeneralResponse> UpdateAsync(Guid id, UpdateTaskDTO model)
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
        return new GeneralResponse(true, "Task updated successfully");
    }

    public async Task<GeneralResponse> DeleteAsync(Guid id)
    {
        var existingTask = await context.Tasks.FindAsync(id);

        if (existingTask == null)
        {
            return null;
            
        }
        
        context.Tasks.Remove(existingTask);
        await context.SaveChangesAsync();

        return new GeneralResponse(true, "Task deleted successfully");
    }
}