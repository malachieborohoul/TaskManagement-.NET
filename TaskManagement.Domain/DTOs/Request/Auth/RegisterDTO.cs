using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.Auth;

public class RegisterDTO:LoginDTO
{
    public string Name { get; set; }
    
   
    public string Role { get; set; } = string.Empty;
}