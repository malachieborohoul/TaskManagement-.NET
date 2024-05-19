using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request.Task;

public class UpdateTaskDTO
{
    [Required]
    public string Title { get; set; }

    public string Description { get; set; }
    [Required]

    public DateTime DueDate { get; set; }
    [Required]

    public Guid StatusId { get; set; }
    [Required]

    public Guid PriorityId { get; set; }
}