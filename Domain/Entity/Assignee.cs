using Domain.Entity.Authentication;

namespace Domain.Entity.Tasks;

public class Assignee
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid TaskId { get; set; }

    public ApplicationUser User { get; set; }

    public Tasks Tasks { get; set; }

}