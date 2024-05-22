using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.SubTask;
using TaskManagement.Application.DTOs.Request.Task;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.DTOs.Response.SubTask;
using TaskManagement.Domain.Entity.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repos;

public class SubTaskRepository(AppDbContext context):ISubTask
{
    public async Task<List<GetSubTaskDTO>> GetAllAsync()
    {
        var subtasks = (await context.SubTasks.Include(s=>s.Tasks).ToListAsync());

        return subtasks.Select(subtask => new GetSubTaskDTO()
        {
            Id = subtask.Id,
            Title = subtask.Tag,
            Tag = subtask.Tag,
            TaskId = subtask.TaskId
        }).ToList();
    }

    public async Task<GetSubTaskDTO?> GetByIdAsync(Guid id)
    {
    
        // Requêter la base de données pour récupérer la sous-tâche avec le subTaskId
        var subTask = await context.SubTasks.FindAsync(id);

        // Vérifier si la sous-tâche existe
        if (subTask == null)
        {
            return null;
        }

        // Mapper la sous-tâche au DTO approprié
        var subTaskDTO = subTask.Adapt<GetSubTaskDTO>();

        return subTaskDTO;

    }

    public async Task<List<GetSubTaskDTO?>> GetAllByTaskIdAsync(Guid id)
    {
        var existingSubTask = await context.Tasks.FindAsync(id);

        if (existingSubTask == null)
        {
            return null;
            
        }
        var subTasks = await context.SubTasks
            .Where(st => st.TaskId == id)
            .ToListAsync();

        // Mapper les sous-tâches aux DTO appropriés
        var subTaskDTOs = subTasks.Adapt<List<GetSubTaskDTO>>();
        return subTaskDTOs;
    }

    public async Task<GeneralResponse> CreateAsync(CreateSubTaskDTO model)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // Vérifier si la tâche existe
            var taskExists = await context.Tasks.AnyAsync(t => t.Id == model.TaskId);
            if (!taskExists)
            {
                return new GeneralResponse(false, "Task does not exist.");
            }

            // Créer la sous-tâche
            var subTaskEntity = model.Adapt<SubTask>();
            context.SubTasks.Add(subTaskEntity);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
            return new GeneralResponse(true, "SubTask created successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Log the exception
            return new GeneralResponse(false, "An error occurred while creating the subtask: " + ex.Message);
        }

        
    }

    public async Task<GeneralResponse> UpdateAsync(Guid id, UpdateSubTaskDTO model)
    {
        var subTaskEntity = await context.SubTasks.FindAsync(id);
        if (subTaskEntity == null)
        {
            return new GeneralResponse(false, "Sub Task not found");
        }

        subTaskEntity = model.Adapt(subTaskEntity);
        context.SubTasks.Update(subTaskEntity);
        await context.SaveChangesAsync();


        return new GeneralResponse(true, "SubTask updated successfully");
    }

    public async Task<GeneralResponse> DeleteAsync(Guid id)
    {
        var subTaskEntity = await context.Tasks.FindAsync(id);
        if (subTaskEntity == null)
        {
            return new GeneralResponse(false, "Sub Task not found");
        }
        
        context.Tasks.Remove(subTaskEntity);
        await context.SaveChangesAsync();
        return new GeneralResponse(true, "SubTask deleted successfully");

    }
}