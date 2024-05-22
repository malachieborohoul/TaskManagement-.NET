using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs.Request.Status;

public class CreateStatusDTO
{
    [Required]public string Name { get; set; }
    [Required] public string Slug { get; set; }
}