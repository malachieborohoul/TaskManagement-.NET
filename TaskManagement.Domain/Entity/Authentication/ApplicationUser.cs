using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Entity.Tasks;

namespace TaskManagement.Domain.Entity.Authentication;

public class ApplicationUser:IdentityUser
{
    public string? Name { get; set; }
    
     
    // Navigation properties
    [JsonIgnore]
    public ICollection<Tasks.Tasks> Tasks { get; set; } = new List<Tasks.Tasks>();
    
    [JsonIgnore]
    public ICollection<Assignee> Assignees { get; set; } = new List<Assignee>();

}