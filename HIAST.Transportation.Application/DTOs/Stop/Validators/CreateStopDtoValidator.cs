using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Stop.Validators;

public class CreateStopDtoValidator : AbstractValidator<CreateStopDto>
{
    public CreateStopDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("{PropertyName} must be between -90 and 90");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("{PropertyName} must be between -180 and 180");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters");
    }
}