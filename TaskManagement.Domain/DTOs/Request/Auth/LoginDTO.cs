using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.DTOs.Request.Auth;

public class LoginDTO
{
   
    public string EmailAddress { get; set; } = string.Empty;

   
    public string Password { get; set; } = string.Empty;
}