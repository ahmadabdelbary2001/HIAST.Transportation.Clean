using FluentValidation;

namespace HIAST.Transportation.Application.DTOs.Trip.Validators;

public class UpdateTripDtoValidator : AbstractValidator<UpdateTripDto>
{
    public UpdateTripDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.LineId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.BusId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.ScheduledTime)
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("{PropertyName} must be a valid trip status");
    }
}