using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.DeleteStop;

public class DeleteStopCommandHandler : IRequestHandler<DeleteStopCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteStopCommand request, CancellationToken cancellationToken)
    {
        var stop = await _unitOfWork.StopRepository.GetByIdAsync(request.Id);
        if (stop == null)
            throw new NotFoundException(nameof(Domain.Entities.Stop), request.Id);

        await _unitOfWork.StopRepository.DeleteAsync(stop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}