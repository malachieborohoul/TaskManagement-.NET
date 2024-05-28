using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.User;

public class UpdateUserDTO
{
    public string Name { get; set; }
  
    public string EmailAddress { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

}