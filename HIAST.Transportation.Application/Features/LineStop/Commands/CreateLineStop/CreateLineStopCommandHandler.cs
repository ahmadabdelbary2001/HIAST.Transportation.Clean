using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineStop.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Commands.CreateLineStop;

public class CreateLineStopCommandHandler : IRequestHandler<CreateLineStopCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLineStopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLineStopCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLineStopDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineStopDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var lineStop = _mapper.Map<Domain.Entities.LineStop>(request.LineStopDto);

        await _unitOfWork.LineStopRepository.CreateAsync(lineStop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return lineStop.Id;
    }
}