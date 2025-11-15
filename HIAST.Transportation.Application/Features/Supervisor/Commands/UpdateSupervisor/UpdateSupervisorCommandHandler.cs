using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Supervisor.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.UpdateSupervisor;

public class UpdateSupervisorCommandHandler : IRequestHandler<UpdateSupervisorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSupervisorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateSupervisorCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSupervisorDtoValidator();
        var validationResult = await validator.ValidateAsync(request.SupervisorDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);

        var supervisor = await _unitOfWork.SupervisorRepository.GetByIdAsync(request.SupervisorDto.Id);
        if (supervisor == null)
            throw new NotFoundException(nameof(Domain.Entities.Supervisor), request.SupervisorDto.Id);

        _mapper.Map(request.SupervisorDto, supervisor);
        
        await _unitOfWork.SupervisorRepository.UpdateAsync(supervisor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}