using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Bus;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Queries.GetBusList;

public class GetBusListQueryHandler : IRequestHandler<GetBusListQuery, List<BusListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBusListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<BusListDto>> Handle(GetBusListQuery request, CancellationToken cancellationToken)
    {
        var buses = await _unitOfWork.BusRepository.GetAllAsync();
        return _mapper.Map<List<BusListDto>>(buses);
    }
}