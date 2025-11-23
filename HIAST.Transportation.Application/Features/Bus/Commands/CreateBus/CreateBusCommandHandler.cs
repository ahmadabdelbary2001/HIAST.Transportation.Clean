using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Bus.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.CreateBus;

public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateBusCommandHandler> _logger;

    public CreateBusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<CreateBusCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateBusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting bus creation process");

        var validator = new CreateBusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.BusDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Bus creation failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Bus", validationResult);
        }

        var bus = _mapper.Map<Domain.Entities.Bus>(request.BusDto);
        
        _logger.LogInformation("Creating bus with license plate: {LicensePlate}", bus.LicensePlate);
        
        await _unitOfWork.BusRepository.CreateAsync(bus);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Bus created successfully with ID: {BusId}", bus.Id);
        return bus.Id;
    }
}