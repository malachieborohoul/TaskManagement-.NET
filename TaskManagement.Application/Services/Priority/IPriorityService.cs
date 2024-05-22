using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Application.Services;

public interface IPriorityService
{
    Task<IEnumerable<Priority>> GetPrioritiesAsync();

}