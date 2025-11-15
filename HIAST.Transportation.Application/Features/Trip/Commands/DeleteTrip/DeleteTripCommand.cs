using MediatR;

namespace HIAST.Transportation.Application.Features.Trip.Commands.DeleteTrip;

public class DeleteTripCommand : IRequest<Unit>
{
    public int Id { get; set; }
}