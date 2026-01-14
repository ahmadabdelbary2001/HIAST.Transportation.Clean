using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.LineSubscription.Validators;

public class CreateLineSubscriptionDtoValidator : AbstractValidator<CreateLineSubscriptionDto>
{
    public CreateLineSubscriptionDtoValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(x => x.LineId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required");
        
        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("{PropertyName} is required.");
    }
}