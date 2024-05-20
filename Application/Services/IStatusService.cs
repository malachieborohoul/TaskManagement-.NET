using Domain.Entity.Tasks;

namespace Application.Services;

public interface IStatusService
{
    Task<IEnumerable<Status>> GetStatusAsync();

}