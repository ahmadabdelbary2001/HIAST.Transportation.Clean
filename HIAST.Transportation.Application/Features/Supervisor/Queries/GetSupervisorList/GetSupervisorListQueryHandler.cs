using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorList;

public class GetSupervisorListQueryHandler : IRequestHandler<GetSupervisorListQuery, List<SupervisorListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetSupervisorListQueryHandler> _logger;

    public GetSupervisorListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetSupervisorListQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<SupervisorListDto>> Handle(GetSupervisorListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all supervisors");

        var supervisors = await _unitOfWork.SupervisorRepository.GetAllAsync();
        
        _logger.LogInformation("Successfully fetched {SupervisorCount} supervisors", supervisors.Count);
        return _mapper.Map<List<SupervisorListDto>>(supervisors);
    }
}