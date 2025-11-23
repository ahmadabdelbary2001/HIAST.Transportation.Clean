using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.DeleteLineSubscription;

public class DeleteLineSubscriptionCommandHandler : IRequestHandler<DeleteLineSubscriptionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<DeleteLineSubscriptionCommandHandler> _logger;

    public DeleteLineSubscriptionCommandHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteLineSubscriptionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLineSubscriptionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting line subscription deletion process for ID: {LineSubscriptionId}", request.Id);

        var lineSubscription = await _unitOfWork.LineSubscriptionRepository.GetByIdAsync(request.Id);
        if (lineSubscription == null)
        {
            _logger.LogWarning("Line subscription not found for deletion with ID: {LineSubscriptionId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.LineSubscription), request.Id);
        }

        _logger.LogInformation("Deleting line subscription with ID: {LineSubscriptionId} for line ID: {LineId} and employee ID: {EmployeeId}", 
            lineSubscription.Id, lineSubscription.LineId, lineSubscription.EmployeeId);

        await _unitOfWork.LineSubscriptionRepository.DeleteAsync(lineSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Line subscription deleted successfully with ID: {LineSubscriptionId}", lineSubscription.Id);
        return Unit.Value;
    }
}