using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Bus.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.UpdateBus;

public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateBusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.BusDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid Bus", validationResult);

        var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.BusDto.Id);
        if (bus == null)
            throw new NotFoundException(nameof(Domain.Entities.Bus), request.BusDto.Id);

        _mapper.Map(request.BusDto, bus);
        
        await _unitOfWork.BusRepository.UpdateAsync(bus);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}