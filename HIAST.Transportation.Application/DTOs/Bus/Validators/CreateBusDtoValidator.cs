using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Bus.Validators;

public class CreateBusDtoValidator : AbstractValidator<CreateBusDto>
{
    public CreateBusDtoValidator()
    {
        RuleFor(x => x.LicensePlate)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("{PropertyName} must be a valid bus status");
    }
}