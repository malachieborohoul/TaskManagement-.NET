using FluentValidation;
using TaskManagement.Domain.DTOs.Request.Auth;

namespace TaskManagement.Application.Validations.Auth;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDTO>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name must be less than 50 characters.");
    }
}