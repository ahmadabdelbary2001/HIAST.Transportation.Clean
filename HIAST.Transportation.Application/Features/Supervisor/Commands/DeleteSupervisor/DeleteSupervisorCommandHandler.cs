using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.DeleteSupervisor;

public class DeleteSupervisorCommandHandler : IRequestHandler<DeleteSupervisorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSupervisorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteSupervisorCommand request, CancellationToken cancellationToken)
    {
        var supervisor = await _unitOfWork.SupervisorRepository.GetByIdAsync(request.Id);
        if (supervisor == null)
            throw new NotFoundException(nameof(Domain.Entities.Supervisor), request.Id);

        await _unitOfWork.SupervisorRepository.DeleteAsync(supervisor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}