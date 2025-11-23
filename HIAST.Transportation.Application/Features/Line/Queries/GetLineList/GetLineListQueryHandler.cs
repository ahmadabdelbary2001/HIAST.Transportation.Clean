using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Line;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineList;

public class GetLineListQueryHandler : IRequestHandler<GetLineListQuery, List<LineListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLineListQueryHandler> _logger;

    public GetLineListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetLineListQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LineListDto>> Handle(GetLineListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all lines");

        var lines = await _unitOfWork.LineRepository.GetAllAsync();
        
        _logger.LogInformation("Successfully fetched {LineCount} lines", lines.Count);
        return _mapper.Map<List<LineListDto>>(lines);
    }
}