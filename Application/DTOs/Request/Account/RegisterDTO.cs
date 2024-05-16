using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request.Account;

public class RegisterDTO:LoginDTO
{
    [Required] public string Name { get; set; }
    [Required] [Compare(nameof(Password))] public string ConfirmPassword { get; set; } = string.Empty;
}