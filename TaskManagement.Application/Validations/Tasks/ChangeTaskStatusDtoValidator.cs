using FluentValidation;
using TaskManagement.Domain.DTOs.Request.Task;

namespace TaskManagement.Application.Validations.Tasks;

public class ChangeTaskStatusDtoValidator:AbstractValidator<ChangeTaskStatusDTO>
{
    public ChangeTaskStatusDtoValidator()
    {
        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("StatusId is required.");

       
    }
}