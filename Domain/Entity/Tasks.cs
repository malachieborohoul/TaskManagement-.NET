using System.Text.Json.Serialization;
using Domain.Entity.Authentication;

namespace Domain.Entity.Tasks;

public class Tasks
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    
    // Foreign Keys
    public Guid StatusId { get; set; }
    public Status Status { get; set; }

    public Guid PriorityId { get; set; }
    public Priority Priority { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    [JsonIgnore]
    public ICollection<Assignee> Assignees { get; set; } = new List<Assignee>();
    public ICollection<SubTask> SubTasks { get; set; }
}