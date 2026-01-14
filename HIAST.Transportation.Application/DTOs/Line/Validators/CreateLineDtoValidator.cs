using FluentValidation;
using HIAST.Transportation.Application.Contracts.Persistence;

namespace HIAST.Transportation.Application.DTOs.Line.Validators;

public class CreateLineDtoValidator : AbstractValidator<CreateLineDto>
{
    private readonly ILineRepository _lineRepository;

    public CreateLineDtoValidator(ILineRepository lineRepository)
    {
        _lineRepository = lineRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters");

        RuleFor(x => x.SupervisorId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MustAsync(async (supervisorId, cancellationToken) => !await _lineRepository.IsSupervisorAssignedAsync(supervisorId))
            .WithMessage("Supervisor is already assigned to another line.");

        RuleFor(x => x.BusId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
            .MustAsync(async (busId, cancellationToken) => !await _lineRepository.IsBusAssignedAsync(busId))
            .WithMessage("Bus is already assigned to another line.");

        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
            .MustAsync(async (driverId, cancellationToken) => !await _lineRepository.IsDriverAssignedAsync(driverId))
            .WithMessage("Driver is already assigned to another line.");
    }
}