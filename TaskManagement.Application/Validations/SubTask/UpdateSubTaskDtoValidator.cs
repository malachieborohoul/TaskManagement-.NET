using FluentValidation;
using TaskManagement.Domain.DTOs.Request.SubTask;

namespace TaskManagement.Application.Validations.SubTask;

public class UpdateSubTaskDtoValidator : AbstractValidator<UpdateSubTaskDTO>
{
    public UpdateSubTaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Tag)
            .NotEmpty().WithMessage("Tag is required.")
            .MaximumLength(50).WithMessage("Tag must not exceed 50 characters.");
            
        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt cannot be in the future.");
    }
}