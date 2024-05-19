
using System.Text.Json.Serialization;

namespace Domain.Entity.Tasks;

public class Status
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    
    // Navigation property
    [JsonIgnore]
    public ICollection<Tasks> Tasks { get; set; }
}