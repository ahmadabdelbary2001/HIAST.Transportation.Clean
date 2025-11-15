using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Supervisor;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorDetail;

public class GetSupervisorDetailQueryHandler : IRequestHandler<GetSupervisorDetailQuery, SupervisorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSupervisorDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SupervisorDto> Handle(GetSupervisorDetailQuery request, CancellationToken cancellationToken)
    {
        var supervisor = await _unitOfWork.SupervisorRepository.GetByIdAsync(request.Id);
        if (supervisor == null)
            throw new NotFoundException(nameof(Domain.Entities.Supervisor), request.Id);

        return _mapper.Map<SupervisorDto>(supervisor);
    }
}