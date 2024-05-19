using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request.Task;

public class CreateTaskDTO:UpdateTaskDTO
{
    [Required]

    public string UserId { get; set; }

}