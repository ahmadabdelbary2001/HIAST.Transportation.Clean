using MediatR;
using HIAST.Transportation.Application.DTOs.Line;

namespace HIAST.Transportation.Application.Features.Line.Commands.HandoverSupervisor;

public class HandoverSupervisorCommand : IRequest
{
    public required HandoverSupervisorDto HandoverSupervisorDto { get; set; }
}
