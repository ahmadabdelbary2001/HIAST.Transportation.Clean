using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Driver.Validators;

public class CreateDriverDtoValidator : AbstractValidator<CreateDriverDto>
{
    public CreateDriverDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

        RuleFor(x => x.LicenseExpiryDate)
            .GreaterThan(DateTime.Now).WithMessage("{PropertyName} must be in the future");

        RuleFor(x => x.ContactInfo)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");
    }
}