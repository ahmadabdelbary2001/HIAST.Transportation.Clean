using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Stop.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.UpdateStop;

public class UpdateStopCommandHandler : IRequestHandler<UpdateStopCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateStopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateStopCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateStopDtoValidator();
        var validationResult = await validator.ValidateAsync(request.StopDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var stop = await _unitOfWork.StopRepository.GetByIdAsync(request.StopDto.Id);
        if (stop == null)
            throw new NotFoundException(nameof(Domain.Entities.Stop), request.StopDto.Id);

        _mapper.Map(request.StopDto, stop);
        
        await _unitOfWork.StopRepository.UpdateAsync(stop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}