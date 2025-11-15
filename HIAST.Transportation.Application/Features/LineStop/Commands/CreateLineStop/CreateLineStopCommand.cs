using HIAST.Transportation.Application.DTOs.LineStop;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Commands.CreateLineStop;

public class CreateLineStopCommand : IRequest<int>
{
    public CreateLineStopDto LineStopDto { get; set; } = null!;
}