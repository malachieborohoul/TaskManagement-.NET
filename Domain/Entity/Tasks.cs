using Domain.Entity.Authentication;

namespace Domain.Entity.Tasks;

public class Tasks
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    
    // Foreign Keys
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public ApplicationUser User { get; set; }
}