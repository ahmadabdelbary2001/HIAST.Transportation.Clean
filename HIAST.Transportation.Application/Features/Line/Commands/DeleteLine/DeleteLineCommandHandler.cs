using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.DeleteLine;

public class DeleteLineCommandHandler : IRequestHandler<DeleteLineCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLineCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteLineCommand request, CancellationToken cancellationToken)
    {
        var line = await _unitOfWork.LineRepository.GetByIdAsync(request.Id);
        if (line == null)
            throw new NotFoundException(nameof(Domain.Entities.Line), request.Id);

        await _unitOfWork.LineRepository.DeleteAsync(line);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}