using Application.DTOs.Response.Task;
using Domain.Entity.Tasks;

namespace Application.Services;

public interface ITaskService
{
    Task<IEnumerable<GetTaskDTO>> GetTasksAsync();

}