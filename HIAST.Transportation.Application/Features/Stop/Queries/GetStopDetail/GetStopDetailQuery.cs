using HIAST.Transportation.Application.DTOs.Stop;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Queries.GetStopDetail;

public class GetStopDetailQuery : IRequest<StopDto>
{
    public int Id { get; set; }
}