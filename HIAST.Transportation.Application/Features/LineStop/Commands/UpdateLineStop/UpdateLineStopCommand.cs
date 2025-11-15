using HIAST.Transportation.Application.DTOs.LineStop;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Commands.UpdateLineStop;

public class UpdateLineStopCommand : IRequest<Unit>
{
    public UpdateLineStopDto LineStopDto { get; set; } = null!;
}