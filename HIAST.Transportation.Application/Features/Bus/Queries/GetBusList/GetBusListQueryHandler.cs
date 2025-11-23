using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Bus;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Queries.GetBusList;

public class GetBusListQueryHandler : IRequestHandler<GetBusListQuery, List<BusListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetBusListQueryHandler> _logger;

    public GetBusListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetBusListQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<BusListDto>> Handle(GetBusListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all buses");

        var buses = await _unitOfWork.BusRepository.GetAllAsync();
        
        _logger.LogInformation("Successfully fetched {BusCount} buses", buses.Count);
        return _mapper.Map<List<BusListDto>>(buses);
    }
}