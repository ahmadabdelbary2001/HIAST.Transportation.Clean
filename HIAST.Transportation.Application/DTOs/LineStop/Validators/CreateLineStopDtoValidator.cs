using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.LineStop.Validators;

public class CreateLineStopDtoValidator : AbstractValidator<CreateLineStopDto>
{
    public CreateLineStopDtoValidator()
    {
        RuleFor(x => x.LineId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.StopId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.SequenceOrder)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}