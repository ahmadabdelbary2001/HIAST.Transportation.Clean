using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Supervisor.Validators;

public class CreateSupervisorDtoValidator : AbstractValidator<CreateSupervisorDto>
{
    public CreateSupervisorDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

        RuleFor(x => x.ContactInfo)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).When(x => x.EmployeeId.HasValue)
            .WithMessage("{PropertyName} must be greater than 0 when provided");
    }
}