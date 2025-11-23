using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Driver;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Queries.GetDriverList;

public class GetDriverListQueryHandler : IRequestHandler<GetDriverListQuery, List<DriverListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetDriverListQueryHandler> _logger;

    public GetDriverListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetDriverListQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<DriverListDto>> Handle(GetDriverListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all drivers");

        var drivers = await _unitOfWork.DriverRepository.GetAllAsync();
        
        _logger.LogInformation("Successfully fetched {DriverCount} drivers", drivers.Count);
        return _mapper.Map<List<DriverListDto>>(drivers);
    }
}