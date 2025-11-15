using HIAST.Transportation.Application.DTOs.Trip;
using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Commands.UpdateTrip;

public class UpdateTripCommand : IRequest<Unit>
{
    public UpdateTripDto TripDto { get; set; } = null!;
}