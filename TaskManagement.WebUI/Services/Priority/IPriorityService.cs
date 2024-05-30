namespace TaskManagement.WebUI.Services.Priority;

public interface IPriorityService
{
    Task<IEnumerable<Domain.Entity.Tasks.Priority>> GetPrioritiesAsync();

}