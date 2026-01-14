using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Line.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.CreateLine;

public class CreateLineCommandHandler : IRequestHandler<CreateLineCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateLineCommandHandler> _logger;

    public CreateLineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<CreateLineCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateLineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting line creation process");

        var validator = new CreateLineDtoValidator(_unitOfWork.LineRepository);
        var validationResult = await validator.ValidateAsync(request.LineDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Line creation failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Line", validationResult);
        }

        var line = _mapper.Map<Domain.Entities.Line>(request.LineDto);
        
        _logger.LogInformation("Creating line with name: {LineName}", line.Name);

        // Update Bus Status
        var bus = await _unitOfWork.BusRepository.GetByIdAsync(line.BusId);
        if (bus != null)
        {
            bus.Status = Domain.Enums.BusStatus.InService;
            await _unitOfWork.BusRepository.UpdateAsync(bus);
        }

        // Auto-subscribe Supervisor
        line.LineSubscriptions.Add(new Domain.Entities.LineSubscription
        {
            EmployeeUserId = line.SupervisorId,
            StartDate = DateTime.UtcNow,
            IsActive = true
        });

        await _unitOfWork.LineRepository.CreateAsync(line);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Line created successfully with ID: {LineId}", line.Id);
        return line.Id;
    }
}