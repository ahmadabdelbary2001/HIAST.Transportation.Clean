using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Line.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.CreateLine;

public class CreateLineCommandHandler : IRequestHandler<CreateLineCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLineCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLineDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var line = _mapper.Map<Domain.Entities.Line>(request.LineDto);

        await _unitOfWork.LineRepository.CreateAsync(line);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return line.Id;
    }
}