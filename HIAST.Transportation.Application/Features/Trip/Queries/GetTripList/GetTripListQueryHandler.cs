using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Trip;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Queries.GetTripList;

public class GetTripListQueryHandler : IRequestHandler<GetTripListQuery, List<TripListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTripListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TripListDto>> Handle(GetTripListQuery request, CancellationToken cancellationToken)
    {
        var trips = await _unitOfWork.TripRepository.GetAllAsync();
        return _mapper.Map<List<TripListDto>>(trips);
    }
}