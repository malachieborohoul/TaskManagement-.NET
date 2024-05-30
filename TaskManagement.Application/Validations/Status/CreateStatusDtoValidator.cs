using FluentValidation;
using TaskManagement.Domain.DTOs.Request.Status;

namespace TaskManagement.Application.Validations.Status;

public class CreateStatusDtoValidator : AbstractValidator<CreateStatusDTO>
{
    public CreateStatusDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required.")
            .MaximumLength(50).WithMessage("Slug must not exceed 50 characters.")
            .Matches("^[a-z0-9-]+$").WithMessage("Slug must contain only lowercase letters, numbers, and hyphens.");
        
        
    }
}