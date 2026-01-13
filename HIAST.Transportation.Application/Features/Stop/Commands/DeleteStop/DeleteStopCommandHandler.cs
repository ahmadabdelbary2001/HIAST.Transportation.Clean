using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.DeleteStop;

public class DeleteStopCommandHandler : IRequestHandler<DeleteStopCommand, Unit>
{
    private readonly IStopRepository _stopRepository;
    private readonly IAppLogger<DeleteStopCommandHandler> _logger;

    public DeleteStopCommandHandler(IStopRepository stopRepository, IAppLogger<DeleteStopCommandHandler> logger)
    {
        _stopRepository = stopRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteStopCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting stop deletion process for ID: {StopId}", request.Id);

        var stop = await _stopRepository.GetByIdAsync(request.Id);
        if (stop == null)
        {
            _logger.LogWarning("Stop with ID {StopId} not found", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Stop), request.Id);
        }

        // التحقق من أن LineId ليس null
        if (!stop.LineId.HasValue)
        {
            var exception = new BadRequestException("Stop must be assigned to a line before deletion.");
            _logger.LogError(exception, "Stop with ID {StopId} has no Line assigned", request.Id);
            throw exception;
        }

        var lineId = stop.LineId.Value; // استخدام .Value لأننا تأكدنا أنه ليس null
        var deletedSequenceOrder = stop.SequenceOrder;
        var wasTerminus = stop.StopType == Domain.Enums.StopType.Terminus;

        // حذف المحطة
        _logger.LogInformation("Deleting stop ID: {StopId}", stop.Id);
        await _stopRepository.DeleteAsync(stop);

        // استخدام الدالة الجديدة لإعادة الترتيب
        await _stopRepository.ReorderStopsAfterDeletionAsync(lineId, deletedSequenceOrder, wasTerminus);

        _logger.LogInformation("Stop deleted successfully and remaining stops reordered for line ID: {LineId}", lineId);
        return Unit.Value;
    }
}