using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.Authentication;

public class ApplicationUser:IdentityUser
{
    public string? Name { get; set; }
    
    // Navigation property
    [JsonIgnore]

    public ICollection<Tasks.Tasks> Tasks { get; set; }

}