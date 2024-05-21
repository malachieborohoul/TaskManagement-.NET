using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request.SubTask;

public class CreateSubTaskDTO:UpdateSubTaskDTO
{
 
   
    [Required]

    public Guid TaskId { get; set; }
}