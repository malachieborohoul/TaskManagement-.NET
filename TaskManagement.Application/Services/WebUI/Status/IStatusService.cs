namespace TaskManagement.Application.Services.WebUI.Status;

public interface IStatusService
{
    Task<IEnumerable<Domain.Entity.Tasks.Status>> GetStatusAsync();

}