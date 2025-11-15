using HIAST.Transportation.Application.DTOs.Trip;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Commands.CreateTrip;

public class CreateTripCommand : IRequest<int>
{
    public CreateTripDto TripDto { get; set; } = null!;
}