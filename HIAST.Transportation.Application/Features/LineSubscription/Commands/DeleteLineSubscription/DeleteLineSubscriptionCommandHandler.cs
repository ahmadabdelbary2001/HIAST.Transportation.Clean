using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.DeleteLineSubscription;

public class DeleteLineSubscriptionCommandHandler : IRequestHandler<DeleteLineSubscriptionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLineSubscriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteLineSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var lineSubscription = await _unitOfWork.LineSubscriptionRepository.GetByIdAsync(request.Id);
        if (lineSubscription == null)
            throw new NotFoundException(nameof(Domain.Entities.LineSubscription), request.Id);

        await _unitOfWork.LineSubscriptionRepository.DeleteAsync(lineSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}