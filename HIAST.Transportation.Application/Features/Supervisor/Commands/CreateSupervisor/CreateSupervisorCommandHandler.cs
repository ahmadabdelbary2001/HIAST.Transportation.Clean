using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Supervisor.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.CreateSupervisor;

public class CreateSupervisorCommandHandler : IRequestHandler<CreateSupervisorCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSupervisorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateSupervisorCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSupervisorDtoValidator();
        var validationResult = await validator.ValidateAsync(request.SupervisorDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid Supervisor", validationResult);

        var supervisor = _mapper.Map<Domain.Entities.Supervisor>(request.SupervisorDto);

        await _unitOfWork.SupervisorRepository.CreateAsync(supervisor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return supervisor.Id;
    }
}