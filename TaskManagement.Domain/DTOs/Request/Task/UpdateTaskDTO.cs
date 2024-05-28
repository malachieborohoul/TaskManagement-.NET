using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.Task;

public class UpdateTaskDTO
{
    public string Title { get; set; }

     public DateTime DueDate { get; set; } = DateTime.UtcNow;

    public Guid StatusId { get; set; }

    public Guid PriorityId { get; set; }
    public List<string> assignees { get; set; }
}