using FluentValidation;
using TaskManagement.Domain.DTOs.Request.Auth;

namespace TaskManagement.Application.Validations.Auth;

public class ChangeUserRoleDtoValidator:AbstractValidator<ChangeUserRoleRequestDTO>
{
    public ChangeUserRoleDtoValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty().WithMessage("User email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name must be less than 50 characters.");
    }
}