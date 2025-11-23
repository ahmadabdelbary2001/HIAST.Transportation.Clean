using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Bus.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.UpdateBus;

public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateBusCommandHandler> _logger;

    public UpdateBusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<UpdateBusCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting bus update process for ID: {BusId}", request.BusDto.Id);

        var validator = new UpdateBusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.BusDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Bus update failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Bus", validationResult);
        }

        var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.BusDto.Id);
        if (bus == null)
        {
            _logger.LogWarning("Bus not found with ID: {BusId}", request.BusDto.Id);
            throw new NotFoundException(nameof(Domain.Entities.Bus), request.BusDto.Id);
        }

        _logger.LogInformation("Updating bus with ID: {BusId}", bus.Id);
        
        _mapper.Map(request.BusDto, bus);
        await _unitOfWork.BusRepository.UpdateAsync(bus);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Bus updated successfully with ID: {BusId}", bus.Id);
        return Unit.Value;
    }
}