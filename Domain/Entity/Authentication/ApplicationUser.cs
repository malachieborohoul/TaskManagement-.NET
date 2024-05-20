using System.Text.Json.Serialization;
using Domain.Entity.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.Authentication;

public class ApplicationUser:IdentityUser
{
    public string? Name { get; set; }
    
     
    // Navigation properties
    [JsonIgnore]
    public ICollection<Tasks.Tasks> Tasks { get; set; } = new List<Tasks.Tasks>();
    
    [JsonIgnore]
    public ICollection<Assignee> Assignees { get; set; } = new List<Assignee>();

}