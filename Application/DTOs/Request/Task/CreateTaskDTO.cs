using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request.Task;

public class CreateTaskDTO:UpdateTaskDTO
{

    public string UserId { get; set; }

}