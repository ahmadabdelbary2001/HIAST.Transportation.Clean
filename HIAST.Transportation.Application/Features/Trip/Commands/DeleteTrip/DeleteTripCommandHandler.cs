using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Commands.DeleteTrip;

public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTripCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.Id);
        if (trip == null)
            throw new NotFoundException(nameof(Domain.Entities.Trip), request.Id);

        await _unitOfWork.TripRepository.DeleteAsync(trip);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}