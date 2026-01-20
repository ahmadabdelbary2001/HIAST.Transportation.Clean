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
            .GreaterThanOrEqualTo(12).WithMessage("Bus capacity must be at least 12 seats")
            .LessThanOrEqualTo(36).WithMessage("Bus capacity must not exceed 36 seats");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("{PropertyName} must be a valid bus status");
    }
}