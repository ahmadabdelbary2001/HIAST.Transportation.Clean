using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Line.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.UpdateLine;

public class UpdateLineCommandHandler : IRequestHandler<UpdateLineCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLineCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLineDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid Line", validationResult);

        var line = await _unitOfWork.LineRepository.GetByIdAsync(request.LineDto.Id);
        if (line == null)
            throw new NotFoundException(nameof(Domain.Entities.Line), request.LineDto.Id);

        _mapper.Map(request.LineDto, line);
        await _unitOfWork.LineRepository.UpdateAsync(line);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}