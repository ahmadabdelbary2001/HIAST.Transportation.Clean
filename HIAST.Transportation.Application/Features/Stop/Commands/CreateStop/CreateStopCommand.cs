using HIAST.Transportation.Application.DTOs.Stop;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.CreateStop;

public class CreateStopCommand : IRequest<int>
{
    public CreateStopDto StopDto { get; set; } = null!;
}