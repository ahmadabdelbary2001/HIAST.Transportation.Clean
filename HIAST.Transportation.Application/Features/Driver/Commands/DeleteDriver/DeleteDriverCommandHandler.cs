using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.DeleteDriver;

public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDriverCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(request.Id);
        if (driver == null)
            throw new NotFoundException(nameof(Domain.Entities.Driver), request.Id);

        await _unitOfWork.DriverRepository.DeleteAsync(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}