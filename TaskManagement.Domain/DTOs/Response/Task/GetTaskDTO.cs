using TaskManagement.Application.DTOs.Response.User;
using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Domain.DTOs.Response.Task;

public class GetTaskDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    
    
    public Status Status { get; set; }

    public Priority Priority { get; set; }

    public GetUserDTO User { get; set; }
    public List<GetUserDTO> Assignees { get; set; }
}