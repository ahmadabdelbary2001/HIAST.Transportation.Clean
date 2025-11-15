using HIAST.Transportation.Application.DTOs.Line;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.UpdateLine;

public class UpdateLineCommand : IRequest<Unit>
{
    public UpdateLineDto LineDto { get; set; } = null!;
}