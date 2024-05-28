using FluentValidation;
using TaskManagement.Domain.DTOs.Request.Task;

namespace TaskManagement.Application.Validations.Tasks;

public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDTO>
{
    public UpdateTaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("DueDate is required.");

        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("StatusId is required.");

        RuleFor(x => x.PriorityId)
            .NotEmpty().WithMessage("PriorityId is required.");

        RuleFor(x => x.assignees)
            .NotEmpty().WithMessage("Assignees list cannot be empty.");
    }
}