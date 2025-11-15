using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Trip;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Queries.GetTripDetail;

public class GetTripDetailQueryHandler : IRequestHandler<GetTripDetailQuery, TripDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTripDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TripDto> Handle(GetTripDetailQuery request, CancellationToken cancellationToken)
    {
        var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.Id);
        if (trip == null)
            throw new NotFoundException(nameof(Domain.Entities.Trip), request.Id);

        return _mapper.Map<TripDto>(trip);
    }
}