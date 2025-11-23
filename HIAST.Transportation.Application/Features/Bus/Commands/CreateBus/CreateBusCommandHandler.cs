using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Bus.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.CreateBus;

public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateBusCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateBusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.BusDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid Bus", validationResult);

        var bus = _mapper.Map<Domain.Entities.Bus>(request.BusDto);

        await _unitOfWork.BusRepository.CreateAsync(bus);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bus.Id;
    }
}