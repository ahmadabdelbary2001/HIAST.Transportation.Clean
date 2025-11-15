using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Line.Validators;

public class UpdateLineDtoValidator : AbstractValidator<UpdateLineDto>
{
    public UpdateLineDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters");

        RuleFor(x => x.SupervisorId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}