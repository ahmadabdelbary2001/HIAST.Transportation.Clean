using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Stop.Validators;

public class UpdateStopDtoValidator : AbstractValidator<UpdateStopDto>
{
    public UpdateStopDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be a valid ID.");

        RuleFor(x => x.LineId)
            .GreaterThan(0).WithMessage("{PropertyName} must be a valid ID.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(x => x.SequenceOrder)
            .GreaterThan(0).WithMessage("{PropertyName} must be a positive integer.");
    }
}