namespace TaskManagement.Application.Services.WebUI.Priority;

public interface IPriorityService
{
    Task<IEnumerable<Domain.Entity.Tasks.Priority>> GetPrioritiesAsync();

}