using HIAST.Transportation.Application.DTOs.Stop;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.ReorderStops;

public class ReorderStopsCommand : IRequest<Unit>
{
    public ReorderStopsDto ReorderDto { get; set; } = null!;
}
