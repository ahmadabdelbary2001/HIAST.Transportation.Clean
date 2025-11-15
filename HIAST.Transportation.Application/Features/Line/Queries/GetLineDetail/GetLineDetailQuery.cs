using HIAST.Transportation.Application.DTOs.Line;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineDetail;

public class GetLineDetailQuery : IRequest<LineDto>
{
    public int Id { get; set; }
}