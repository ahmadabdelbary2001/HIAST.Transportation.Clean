using HIAST.Transportation.Application.DTOs.LineStop;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Queries.GetLineStopDetail;

public class GetLineStopDetailQuery : IRequest<LineStopDto>
{
    public int Id { get; set; }
}