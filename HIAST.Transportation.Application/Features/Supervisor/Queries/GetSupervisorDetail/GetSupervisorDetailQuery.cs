using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorDetail;

public class GetSupervisorDetailQuery : IRequest<SupervisorDto>
{
    public int Id { get; set; }
}