using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Stop;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Queries.GetStopList;

public class GetStopListQueryHandler : IRequestHandler<GetStopListQuery, List<StopListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStopListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<StopListDto>> Handle(GetStopListQuery request, CancellationToken cancellationToken)
    {
        var stops = await _unitOfWork.StopRepository.GetAllAsync();
        return _mapper.Map<List<StopListDto>>(stops);
    }
}