using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.Status;

public class CreateStatusDTO
{
    public string Name { get; set; }
    public string Slug { get; set; }
}