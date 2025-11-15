using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineStop.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Commands.UpdateLineStop;

public class UpdateLineStopCommandHandler : IRequestHandler<UpdateLineStopCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLineStopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLineStopCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLineStopDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineStopDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var lineStop = await _unitOfWork.LineStopRepository.GetByIdAsync(request.LineStopDto.Id);
        if (lineStop == null)
            throw new NotFoundException(nameof(Domain.Entities.LineStop), request.LineStopDto.Id);

        _mapper.Map(request.LineStopDto, lineStop);
        
        await _unitOfWork.LineStopRepository.UpdateAsync(lineStop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}