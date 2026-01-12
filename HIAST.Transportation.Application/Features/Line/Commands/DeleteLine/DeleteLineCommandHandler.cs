using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.DeleteLine;

public class DeleteLineCommandHandler : IRequestHandler<DeleteLineCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<DeleteLineCommandHandler> _logger;

    public DeleteLineCommandHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteLineCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting line deletion process for ID: {LineId}", request.Id);

        var line = await _unitOfWork.LineRepository.GetByIdAsync(request.Id);
        if (line == null)
        {
            _logger.LogWarning("Line not found for deletion with ID: {LineId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Line), request.Id);
        }

        _logger.LogInformation("Deleting line with ID: {LineId} and name: {LineName}", 
            line.Id, line.Name);

        // Update Bus Status to Available
        var bus = await _unitOfWork.BusRepository.GetByIdAsync(line.BusId);
        if (bus != null)
        {
            bus.Status = Domain.Enums.BusStatus.Available;
            await _unitOfWork.BusRepository.UpdateAsync(bus);
        }

        await _unitOfWork.LineRepository.DeleteAsync(line);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Line deleted successfully with ID: {LineId}", line.Id);
        return Unit.Value;
    }
}