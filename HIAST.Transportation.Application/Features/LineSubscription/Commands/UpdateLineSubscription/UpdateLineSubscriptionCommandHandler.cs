using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.LineSubscription.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.UpdateLineSubscription;

public class UpdateLineSubscriptionCommandHandler : IRequestHandler<UpdateLineSubscriptionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateLineSubscriptionCommandHandler> _logger;

    public UpdateLineSubscriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<UpdateLineSubscriptionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLineSubscriptionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting line subscription update process for ID: {LineSubscriptionId}", request.LineSubscriptionDto.Id);

        var validator = new UpdateLineSubscriptionDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineSubscriptionDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Line subscription update failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid LineSubscription", validationResult);
        }

        var lineSubscription = await _unitOfWork.LineSubscriptionRepository.GetByIdAsync(request.LineSubscriptionDto.Id);
        if (lineSubscription == null)
        {
            _logger.LogWarning("Line subscription not found with ID: {LineSubscriptionId}", request.LineSubscriptionDto.Id);
            throw new NotFoundException(nameof(Domain.Entities.LineSubscription), request.LineSubscriptionDto.Id);
        }

        _logger.LogInformation("Updating line subscription with ID: {LineSubscriptionId}", lineSubscription.Id);
        
        _mapper.Map(request.LineSubscriptionDto, lineSubscription);
        await _unitOfWork.LineSubscriptionRepository.UpdateAsync(lineSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Line subscription updated successfully with ID: {LineSubscriptionId}", lineSubscription.Id);
        return Unit.Value;
    }
}