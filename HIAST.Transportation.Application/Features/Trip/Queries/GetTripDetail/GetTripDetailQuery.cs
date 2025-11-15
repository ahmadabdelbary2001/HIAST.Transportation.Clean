using HIAST.Transportation.Application.DTOs.Trip;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Queries.GetTripDetail;

public class GetTripDetailQuery : IRequest<TripDto>
{
    public int Id { get; set; }
}