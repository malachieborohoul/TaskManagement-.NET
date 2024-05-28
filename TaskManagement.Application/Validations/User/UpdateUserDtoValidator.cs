using FluentValidation;
using TaskManagement.Domain.DTOs.Request.User;

namespace TaskManagement.Application.Validations.User;

public class UpdateUserDtoValidator: AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(6).WithMessage("Name is required.");
        
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .MinimumLength(6).WithMessage("Role is required.");
    }
}