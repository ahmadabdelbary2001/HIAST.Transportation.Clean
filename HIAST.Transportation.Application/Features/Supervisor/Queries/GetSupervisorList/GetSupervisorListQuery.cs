using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorList;

public class GetSupervisorListQuery : IRequest<List<SupervisorListDto>>
{
}