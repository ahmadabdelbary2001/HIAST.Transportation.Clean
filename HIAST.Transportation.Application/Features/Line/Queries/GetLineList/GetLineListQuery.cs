using HIAST.Transportation.Application.DTOs.Line;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineList;

public class GetLineListQuery : IRequest<List<LineListDto>>
{
}