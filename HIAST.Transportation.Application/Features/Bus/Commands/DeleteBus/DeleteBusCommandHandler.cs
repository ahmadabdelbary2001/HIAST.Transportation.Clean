using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.DeleteBus;

public class DeleteBusCommandHandler : IRequestHandler<DeleteBusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<DeleteBusCommandHandler> _logger;

    public DeleteBusCommandHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteBusCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting bus deletion process for ID: {BusId}", request.Id);

        var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.Id);
        if (bus == null)
        {
            _logger.LogWarning("Bus not found for deletion with ID: {BusId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Bus), request.Id);
        }

        _logger.LogInformation("Deleting bus with ID: {BusId} and license plate: {LicensePlate}", 
            bus.Id, bus.LicensePlate);

        await _unitOfWork.BusRepository.DeleteAsync(bus);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Bus deleted successfully with ID: {BusId}", bus.Id);
        return Unit.Value;
    }
}