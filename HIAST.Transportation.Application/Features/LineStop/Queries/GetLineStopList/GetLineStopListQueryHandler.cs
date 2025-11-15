using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineStop;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Queries.GetLineStopList;

public class GetLineStopListQueryHandler : IRequestHandler<GetLineStopListQuery, List<LineStopListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLineStopListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<LineStopListDto>> Handle(GetLineStopListQuery request, CancellationToken cancellationToken)
    {
        var lineStops = await _unitOfWork.LineStopRepository.GetAllAsync();
        return _mapper.Map<List<LineStopListDto>>(lineStops);
    }
}