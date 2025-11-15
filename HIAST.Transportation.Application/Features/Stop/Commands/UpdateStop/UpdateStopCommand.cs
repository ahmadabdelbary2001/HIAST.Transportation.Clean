using HIAST.Transportation.Application.DTOs.Stop;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.UpdateStop;

public class UpdateStopCommand : IRequest<Unit>
{
    public UpdateStopDto StopDto { get; set; } = null!;
}