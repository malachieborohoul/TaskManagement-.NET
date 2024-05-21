using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request.SubTask;

public class UpdateSubTaskDTO
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Tag { get; set; }
    
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

}