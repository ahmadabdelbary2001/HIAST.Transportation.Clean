using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.DeleteLine;

public class DeleteLineCommand : IRequest<Unit>
{
    public int Id { get; set; }
}