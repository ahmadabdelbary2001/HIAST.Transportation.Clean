using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.CreateSupervisor;

public class CreateSupervisorCommand : IRequest<int>
{
    public CreateSupervisorDto SupervisorDto { get; set; } = null!;
}