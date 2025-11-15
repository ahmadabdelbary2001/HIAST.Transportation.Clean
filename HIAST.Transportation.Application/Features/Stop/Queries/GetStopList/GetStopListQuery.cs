using HIAST.Transportation.Application.DTOs.Stop;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Queries.GetStopList;

public class GetStopListQuery : IRequest<List<StopListDto>>
{
}