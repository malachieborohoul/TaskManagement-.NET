using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.SubTask;

public class CreateSubTaskDTO:UpdateSubTaskDTO
{
 
   
    [Required]

    public Guid TaskId { get; set; }
}