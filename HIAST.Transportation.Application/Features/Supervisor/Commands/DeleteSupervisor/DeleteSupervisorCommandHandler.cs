using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.DeleteSupervisor;

public class DeleteSupervisorCommandHandler : IRequestHandler<DeleteSupervisorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<DeleteSupervisorCommandHandler> _logger;

    public DeleteSupervisorCommandHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteSupervisorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteSupervisorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting supervisor deletion process for ID: {SupervisorId}", request.Id);

        var supervisor = await _unitOfWork.SupervisorRepository.GetByIdAsync(request.Id);
        if (supervisor == null)
        {
            _logger.LogWarning("Supervisor not found for deletion with ID: {SupervisorId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Supervisor), request.Id);
        }

        _logger.LogInformation("Deleting supervisor with ID: {SupervisorId} and name: {SupervisorName}", 
            supervisor.Id, supervisor.Name);

        await _unitOfWork.SupervisorRepository.DeleteAsync(supervisor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Supervisor deleted successfully with ID: {SupervisorId}", supervisor.Id);
        return Unit.Value;
    }
}