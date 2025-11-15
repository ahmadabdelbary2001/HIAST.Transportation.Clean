using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.UpdateSupervisor;

public class UpdateSupervisorCommand : IRequest<Unit>
{
    public UpdateSupervisorDto SupervisorDto { get; set; } = null!;
}