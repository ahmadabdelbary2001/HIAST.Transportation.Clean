using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Commands.DeleteSupervisor;

public class DeleteSupervisorCommand : IRequest<Unit>
{
    public int Id { get; set; }
}