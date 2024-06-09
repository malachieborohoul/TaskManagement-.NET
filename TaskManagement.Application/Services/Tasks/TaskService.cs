using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.DTOs.Request.Task;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.Task;
using TaskManagement.Domain.Entities.Authentication;

namespace TaskManagement.Application.Services.Tasks;

public class TaskService(ITaskRepository taskRepository,UserManager<ApplicationUser> userManager):ITaskService
{
    #region Add often used methods
/*
    private async Task<Domain.Entity.Tasks.Status> FindStatusById(Guid id) =>await context.Status.FindAsync(id);
    private async Task<Domain.Entity.Tasks.Priority> FindPriorityById(Guid id) =>await context.Priorities.FindAsync(id);
    private async Task<ApplicationUser> FindUserById(string id) => await userManager.FindByIdAsync(id);
*/
    #endregion
    

    public async Task<List<GetTaskDTO>> GetAllAsync() => await taskRepository.GetAllAsync();
    public async Task<GetTaskDTO?> GetByIdAsync(Guid id) => await taskRepository.GetByIdAsync(id);
    public async Task<Domain.Entity.Tasks.Tasks?> GetAllByStatusIdAsync(Guid id) => await taskRepository.GetAllByStatusIdAsync(id);
    public async Task<Domain.Entity.Tasks.Tasks?> GetAllByPriorityIdAsync(Guid id) => await taskRepository.GetAllByPriorityIdAsync(id);
    public async Task<GeneralResponse> CreateAsync(CreateTaskDTO model) => await taskRepository.CreateAsync(model);
    public async Task<GeneralResponse> UpdateAsync(Guid taskId, UpdateTaskDTO model) => await taskRepository.UpdateAsync(taskId, model);
    public async Task<GeneralResponse> DeleteAsync(Guid id) => await taskRepository.DeleteAsync(id);
    public async Task<GeneralResponse> ChangeTaskStatusAsync(Guid taskId, Guid statusId) => await taskRepository.ChangeTaskStatusAsync(taskId, statusId);
    

}