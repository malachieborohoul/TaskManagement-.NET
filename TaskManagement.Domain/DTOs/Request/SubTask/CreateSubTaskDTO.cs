using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.SubTask;

public class CreateSubTaskDTO:UpdateSubTaskDTO
{
 
   

    public Guid TaskId { get; set; }
}