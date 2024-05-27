namespace TaskManagement.Domain.Entity.Tasks;

public class SubTask
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Tag { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    
    // Foreign Keys
    public Guid TaskId { get; set; }
    public Tasks Tasks { get; set; }
}