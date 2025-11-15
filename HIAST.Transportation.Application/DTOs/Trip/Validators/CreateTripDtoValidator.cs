using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Trip.Validators;

public class CreateTripDtoValidator : AbstractValidator<CreateTripDto>
{
    public CreateTripDtoValidator()
    {
        RuleFor(x => x.LineId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.BusId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.ScheduledTime)
            .GreaterThan(DateTime.Now).WithMessage("{PropertyName} must be in the future");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("{PropertyName} must be a valid trip status");
    }
}