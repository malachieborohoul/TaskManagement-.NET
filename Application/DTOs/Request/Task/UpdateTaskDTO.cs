namespace Application.DTOs.Request.Task;

public class UpdateTaskDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Guid StatusId { get; set; }
    public Guid PriorityId { get; set; }
}