using Domain.Entity.Tasks;

namespace Application.Services;

public interface IPriorityService
{
    Task<IEnumerable<Priority>> GetPrioritiesAsync();

}