using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.LineStop.Validators;

public class UpdateLineStopDtoValidator : AbstractValidator<UpdateLineStopDto>
{
    public UpdateLineStopDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.LineId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.StopId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.SequenceOrder)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}