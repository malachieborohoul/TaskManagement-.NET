using Application.DTOs.Response.User;
using Domain.Entity.Authentication;
using Domain.Entity.Tasks;

namespace Application.DTOs.Response.Task;

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