using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Commands.DeleteLineStop;

public class DeleteLineStopCommandHandler : IRequestHandler<DeleteLineStopCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLineStopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteLineStopCommand request, CancellationToken cancellationToken)
    {
        var lineStop = await _unitOfWork.LineStopRepository.GetByIdAsync(request.Id);
        if (lineStop == null)
            throw new NotFoundException(nameof(Domain.Entities.LineStop), request.Id);

        await _unitOfWork.LineStopRepository.DeleteAsync(lineStop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}