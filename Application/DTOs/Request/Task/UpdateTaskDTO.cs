using System.ComponentModel.DataAnnotations;
using Application.DTOs.Response;

namespace Application.DTOs.Request.Task;

public class UpdateTaskDTO
{
    [Required]
    public string Title { get; set; }

    [Required] public DateTime DueDate { get; set; } = DateTime.UtcNow;
    [Required]

    public Guid StatusId { get; set; }
    [Required]

    public Guid PriorityId { get; set; }
    [Required]
    public List<string> assignees { get; set; }
}