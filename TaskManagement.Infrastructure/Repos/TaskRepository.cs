using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Task;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.DTOs.Response.Task;
using TaskManagement.Application.DTOs.Response.User;
using TaskManagement.Domain.Entity.Authentication;
using TaskManagement.Domain.Entity.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repos;

public class TaskRepository(AppDbContext context,UserManager<ApplicationUser> userManager):ITasks
{
    #region Add often used methods

    private async Task<Status> FindStatusById(Guid id) =>await context.Status.FindAsync(id);
    private async Task<Priority> FindPriorityById(Guid id) =>await context.Priorities.FindAsync(id);
    private async Task<ApplicationUser> FindUserById(string id) => await userManager.FindByIdAsync(id);

    #endregion
    

    public async Task<List<GetTaskDTO>> GetAllAsync()
    {
        var tasks = (await context.Tasks.Include(s=>s.Status).Include(p=>p.Priority).Include(t=>t.User ).Include(t => t.Assignees)
            .ThenInclude(a => a.User).ToListAsync());
        return tasks    .Select(task=> 
            new GetTaskDTO()
        {
            Id=task.Id,
           Title= task.Title,
          CreatedAt = task.CreatedAt,
          DueDate = task.DueDate,
           Status = task.Status,
           Priority = task.Priority,
            User=new GetUserDTO()
            {
                Id = task.User.Id,
                Name = task.User.Name,
                Email = task.User.Email,
            },
            Assignees = task.Assignees.Select(assignee => new GetUserDTO()
            {
            Id = assignee.User.Id,
            Name = assignee.User.Name,
            Email = assignee.User.Email
        }).ToList()
           
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
        using var transaction = await context.Database.BeginTransactionAsync();
    
        try
        {
            // Créer la tâche
            var taskEntity = model.Adapt<Tasks>();
            var task = context.Tasks.Add(taskEntity);
            await context.SaveChangesAsync();

            // Ajouter les assignations
            foreach (var assignee in model.assignees)
            {
                context.Assignees.Add(new Assignee()
                {
                    UserId = assignee, 
                    TaskId = task.Entity.Id
                });
            }

            await context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new GeneralResponse(true, "Task saved successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Log the exception
            return new GeneralResponse(false, "An error occurred while saving the task: " + ex.Message);
        }
    }


    public async Task<GeneralResponse> UpdateAsync(Guid taskId, UpdateTaskDTO model)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // Récupérer la tâche existante
            var taskEntity = await context.Tasks.FindAsync(taskId);
            if (taskEntity == null)
            {
                return new GeneralResponse(false, "Task not found");
            }

            // Mettre à jour les propriétés de la tâche
            taskEntity = model.Adapt(taskEntity);
            context.Tasks.Update(taskEntity);

            // Supprimer les assignés existants pour cette tâche
            var existingAssignees = context.Assignees.Where(a => a.TaskId == taskId);
            context.Assignees.RemoveRange(existingAssignees);
            await context.SaveChangesAsync();

            // Ajouter les nouveaux assignés
            foreach (var assignee in model.assignees)
            {
                context.Assignees.Add(new Assignee()
                {
                    UserId = assignee,
                    TaskId = taskId
                });
            }

            await context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new GeneralResponse(true, "Task updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Log the exception
            return new GeneralResponse(false, "An error occurred while updating the task: " + ex.Message);
        }
    }


    public async Task<GeneralResponse> DeleteAsync(Guid id)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var existingTask = await context.Tasks.FindAsync(id);

            if (existingTask == null)
            {
                return new GeneralResponse(false, "Task not found");
            }

            // Retirer les assignees associés à cette tâche
            var assignees = context.Assignees.Where(a => a.TaskId == id).ToList();
            context.Assignees.RemoveRange(assignees);

            // Retirer la tâche
            context.Tasks.Remove(existingTask);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new GeneralResponse(true, "Task and its assignees deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Log the exception
            return new GeneralResponse(false, "An error occurred while deleting the task: " + ex.Message);
        }
    }
    
    public async Task<GeneralResponse> ChangeTaskStatusAsync(Guid taskId, Guid statusId)
    {
        var task = await context.Tasks.FindAsync(taskId);
        if (task == null)
        {
            return new GeneralResponse(false, "Task not found");
        }

        task.StatusId = statusId;
        context.Tasks.Update(task);
        await context.SaveChangesAsync();
        
        return new GeneralResponse(true, "Status Task has been changed successfully");

    }
    

}