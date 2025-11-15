using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Stop.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.CreateStop;

public class CreateStopCommandHandler : IRequestHandler<CreateStopCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateStopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateStopCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateStopDtoValidator();
        var validationResult = await validator.ValidateAsync(request.StopDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var stop = _mapper.Map<Domain.Entities.Stop>(request.StopDto);

        await _unitOfWork.StopRepository.CreateAsync(stop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return stop.Id;
    }
}