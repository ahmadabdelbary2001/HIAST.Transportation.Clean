using HIAST.Transportation.Application.DTOs.LineStop;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Queries.GetLineStopList;

public class GetLineStopListQuery : IRequest<List<LineStopListDto>>
{
}