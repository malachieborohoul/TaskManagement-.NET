namespace TaskManagement.Domain.DTOs.Response.SubTask;

public class GetSubTaskDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Tag { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public Guid TaskId { get; set; }
}