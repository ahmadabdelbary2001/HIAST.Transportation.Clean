using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorLineAssignments;

public class GetSupervisorLineAssignmentsQuery : IRequest<IReadOnlyList<SupervisorLineDto>>
{
}