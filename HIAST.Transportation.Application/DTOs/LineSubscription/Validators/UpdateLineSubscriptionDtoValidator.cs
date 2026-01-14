using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.LineSubscription.Validators;

public class UpdateLineSubscriptionDtoValidator : AbstractValidator<UpdateLineSubscriptionDto>
{
    public UpdateLineSubscriptionDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(x => x.LineId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        
        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("{PropertyName} is required.");
        
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("{PropertyName} must be greater than StartDate");
    }
}