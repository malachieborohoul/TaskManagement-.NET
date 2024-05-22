using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Application.Services;

public interface IStatusService
{
    Task<IEnumerable<Status>> GetStatusAsync();

}