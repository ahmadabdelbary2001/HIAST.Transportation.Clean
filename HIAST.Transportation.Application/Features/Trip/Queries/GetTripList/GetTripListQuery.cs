using HIAST.Transportation.Application.DTOs.Trip;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Queries.GetTripList;

public class GetTripListQuery : IRequest<List<TripListDto>>
{
}