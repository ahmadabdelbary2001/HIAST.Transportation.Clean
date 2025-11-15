using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.DeleteBus;

public class DeleteBusCommandHandler : IRequestHandler<DeleteBusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
    {
        var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.Id);
        if (bus == null)
            throw new NotFoundException(nameof(Domain.Entities.Bus), request.Id);

        await _unitOfWork.BusRepository.DeleteAsync(bus);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}