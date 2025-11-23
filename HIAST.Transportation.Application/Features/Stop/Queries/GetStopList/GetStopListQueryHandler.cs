using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Stop;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Queries.GetStopList;

public class GetStopListQueryHandler : IRequestHandler<GetStopListQuery, List<StopListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetStopListQueryHandler> _logger;

    public GetStopListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetStopListQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<StopListDto>> Handle(GetStopListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all stops");

        var stops = await _unitOfWork.StopRepository.GetAllAsync();
        
        _logger.LogInformation("Successfully fetched {StopCount} stops", stops.Count);
        return _mapper.Map<List<StopListDto>>(stops);
    }
}