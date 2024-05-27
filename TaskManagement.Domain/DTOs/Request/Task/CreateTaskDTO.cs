namespace TaskManagement.Domain.DTOs.Request.Task;

public class CreateTaskDTO:UpdateTaskDTO
{

    public string UserId { get; set; }

}