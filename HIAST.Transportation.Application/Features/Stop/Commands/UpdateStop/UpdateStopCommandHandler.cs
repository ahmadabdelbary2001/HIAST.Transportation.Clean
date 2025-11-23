using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Stop.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.UpdateStop;

public class UpdateStopCommandHandler : IRequestHandler<UpdateStopCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateStopCommandHandler> _logger;

    public UpdateStopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<UpdateStopCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateStopCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting stop update process for ID: {StopId}", request.StopDto.Id);

        var validator = new UpdateStopDtoValidator();
        var validationResult = await validator.ValidateAsync(request.StopDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Stop update failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Stop", validationResult);
        }

        var stop = await _unitOfWork.StopRepository.GetByIdAsync(request.StopDto.Id);
        if (stop == null)
        {
            _logger.LogWarning("Stop not found with ID: {StopId}", request.StopDto.Id);
            throw new NotFoundException(nameof(Domain.Entities.Stop), request.StopDto.Id);
        }

        _logger.LogInformation("Updating stop with ID: {StopId}", stop.Id);
        
        _mapper.Map(request.StopDto, stop);
        await _unitOfWork.StopRepository.UpdateAsync(stop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Stop updated successfully with ID: {StopId}", stop.Id);
        return Unit.Value;
    }
}