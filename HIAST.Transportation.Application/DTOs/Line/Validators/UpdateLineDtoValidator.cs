using FluentValidation;
using HIAST.Transportation.Application.Contracts.Persistence;

namespace HIAST.Transportation.Application.DTOs.Line.Validators;

public class UpdateLineDtoValidator : AbstractValidator<UpdateLineDto>
{
    private readonly ILineRepository _lineRepository;

    public UpdateLineDtoValidator(ILineRepository lineRepository)
    {
        _lineRepository = lineRepository;

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters");

        RuleFor(x => x.SupervisorId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MustAsync(async (dto, supervisorId, cancellationToken) => !await _lineRepository.IsSupervisorAssignedAsync(supervisorId, dto.Id))
            .WithMessage("Supervisor is already assigned to another line.");

        RuleFor(x => x.BusId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
            .MustAsync(async (dto, busId, cancellationToken) => !await _lineRepository.IsBusAssignedAsync(busId, dto.Id))
            .WithMessage("Bus is already assigned to another line.");

        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
            .MustAsync(async (dto, driverId, cancellationToken) => !await _lineRepository.IsDriverAssignedAsync(driverId, dto.Id))
            .WithMessage("Driver is already assigned to another line.");
    }
}