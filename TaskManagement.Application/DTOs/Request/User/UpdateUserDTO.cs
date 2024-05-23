using System.ComponentModel.DataAnnotations;
using TaskManagement.Application.DTOs.Request.Account;

namespace TaskManagement.Application.DTOs.Request.User;

public class UpdateUserDTO
{
    [Required] public string Name { get; set; }
    [EmailAddress, Required, DataType(DataType.EmailAddress)]
    [RegularExpression("[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+",
        ErrorMessage = "Your Email is not valid, provide valid email such @gmail")]
    [Display(Name = "Email Address")]
    public string EmailAddress { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

}