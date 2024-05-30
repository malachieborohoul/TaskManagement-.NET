namespace TaskManagement.WebUI.Services.Status;

public interface IStatusService
{
    Task<IEnumerable<Domain.Entity.Tasks.Status>> GetStatusAsync();

}