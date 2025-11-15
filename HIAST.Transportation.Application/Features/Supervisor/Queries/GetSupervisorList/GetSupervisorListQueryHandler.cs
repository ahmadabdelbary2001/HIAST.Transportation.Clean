using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorList;

public class GetSupervisorListQueryHandler : IRequestHandler<GetSupervisorListQuery, List<SupervisorListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSupervisorListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SupervisorListDto>> Handle(GetSupervisorListQuery request, CancellationToken cancellationToken)
    {
        var supervisors = await _unitOfWork.SupervisorRepository.GetAllAsync();
        return _mapper.Map<List<SupervisorListDto>>(supervisors);
    }
}