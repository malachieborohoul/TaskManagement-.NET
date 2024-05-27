using Mapster;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.DTOs.Request.SubTask;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.SubTask;

namespace TaskManagement.Application.Services.API.SubTask;

public class SubTaskService(ISubTaskRepository subTaskRepository, ITaskRepository taskRepository):ISubTaskService
{
    public async Task<List<GetSubTaskDTO>> GetAllAsync()
    {
        var subTasks = await subTaskRepository.GetAllAsync();
        return subTasks.Adapt<List<GetSubTaskDTO>>();
    }

    public async Task<GetSubTaskDTO?> GetByIdAsync(Guid id)
    {
        var subTask = await subTaskRepository.GetByIdAsync(id);
        return subTask?.Adapt<GetSubTaskDTO>();
    }

    public async Task<List<GetSubTaskDTO>> GetAllByTaskIdAsync(Guid taskId)
    {
        var subTasks = await subTaskRepository.GetAllByTaskIdAsync(taskId);
        return subTasks.Adapt<List<GetSubTaskDTO>>();
    }

    public async Task<GeneralResponse> CreateAsync(CreateSubTaskDTO model)
    {
        var taskExists = await taskRepository.GetByIdAsync(model.TaskId) != null;
        if (!taskExists)
        {
            return new GeneralResponse(false, "Task does not exist.");
        }

        var subTaskEntity = model.Adapt<Domain.Entity.Tasks.SubTask>();
        await subTaskRepository.CreateAsync(subTaskEntity);
        return new GeneralResponse(true, "SubTask created successfully.");
    }

    public async Task<GeneralResponse> UpdateAsync(Guid id, UpdateSubTaskDTO model)
    {
        var subTaskEntity = await subTaskRepository.GetByIdAsync(id);
        if (subTaskEntity == null)
        {
            return new GeneralResponse(false, "SubTask not found");
        }

        model.Adapt(subTaskEntity);
        await subTaskRepository.UpdateAsync(subTaskEntity);
        return new GeneralResponse(true, "SubTask updated successfully");
    }

    public async Task<GeneralResponse> DeleteAsync(Guid id)
    {
        var success = await subTaskRepository.DeleteAsync(id);
        return success 
            ? new GeneralResponse(true, "SubTask deleted successfully")
            : new GeneralResponse(false, "SubTask not found");
    }
}