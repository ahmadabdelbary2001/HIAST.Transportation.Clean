using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Supervisor.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.CreateSupervisor;

public class CreateSupervisorCommandHandler : IRequestHandler<CreateSupervisorCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateSupervisorCommandHandler> _logger;

    public CreateSupervisorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<CreateSupervisorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateSupervisorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting supervisor creation process");

        var validator = new CreateSupervisorDtoValidator();
        var validationResult = await validator.ValidateAsync(request.SupervisorDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Supervisor creation failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Supervisor", validationResult);
        }

        var supervisor = _mapper.Map<Domain.Entities.Supervisor>(request.SupervisorDto);
        
        _logger.LogInformation("Creating supervisor with name: {SupervisorName}", supervisor.Name);

        await _unitOfWork.SupervisorRepository.CreateAsync(supervisor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Supervisor created successfully with ID: {SupervisorId}", supervisor.Id);
        return supervisor.Id;
    }
}