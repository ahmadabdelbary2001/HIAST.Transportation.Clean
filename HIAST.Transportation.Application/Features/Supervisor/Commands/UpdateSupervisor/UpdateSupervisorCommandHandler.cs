using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Supervisor.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.UpdateSupervisor;

public class UpdateSupervisorCommandHandler : IRequestHandler<UpdateSupervisorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateSupervisorCommandHandler> _logger;

    public UpdateSupervisorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<UpdateSupervisorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateSupervisorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting supervisor update process for ID: {SupervisorId}", request.SupervisorDto.Id);

        var validator = new UpdateSupervisorDtoValidator();
        var validationResult = await validator.ValidateAsync(request.SupervisorDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Supervisor update failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Supervisor", validationResult);
        }

        var supervisor = await _unitOfWork.SupervisorRepository.GetByIdAsync(request.SupervisorDto.Id);
        if (supervisor == null)
        {
            _logger.LogWarning("Supervisor not found with ID: {SupervisorId}", request.SupervisorDto.Id);
            throw new NotFoundException(nameof(Domain.Entities.Supervisor), request.SupervisorDto.Id);
        }

        _logger.LogInformation("Updating supervisor with ID: {SupervisorId}", supervisor.Id);
        
        _mapper.Map(request.SupervisorDto, supervisor);
        await _unitOfWork.SupervisorRepository.UpdateAsync(supervisor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Supervisor updated successfully with ID: {SupervisorId}", supervisor.Id);
        return Unit.Value;
    }
}