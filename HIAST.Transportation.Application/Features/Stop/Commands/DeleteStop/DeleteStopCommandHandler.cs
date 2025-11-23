using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.DeleteStop;

public class DeleteStopCommandHandler : IRequestHandler<DeleteStopCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<DeleteStopCommandHandler> _logger;

    public DeleteStopCommandHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteStopCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteStopCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting stop deletion process for ID: {StopId}", request.Id);

        var stop = await _unitOfWork.StopRepository.GetByIdAsync(request.Id);
        if (stop == null)
        {
            _logger.LogWarning("Stop not found for deletion with ID: {StopId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Stop), request.Id);
        }

        _logger.LogInformation("Deleting stop with ID: {StopId} and address: {Address}", 
            stop.Id, stop.Address);

        await _unitOfWork.StopRepository.DeleteAsync(stop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Stop deleted successfully with ID: {StopId}", stop.Id);
        return Unit.Value;
    }
}