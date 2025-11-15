using HIAST.Transportation.Application.DTOs.Line;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.CreateLine;

public class CreateLineCommand : IRequest<int>
{
    public CreateLineDto LineDto { get; set; } = null!;
}