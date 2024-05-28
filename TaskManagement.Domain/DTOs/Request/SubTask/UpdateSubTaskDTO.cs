using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.SubTask;

public class UpdateSubTaskDTO
{
    public string Title { get; set; }
    
    public string Tag { get; set; }
    
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

}