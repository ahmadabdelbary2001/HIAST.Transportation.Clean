using MediatR;

namespace HIAST.Transportation.Application.Features.LineStop.Commands.DeleteLineStop;

public class DeleteLineStopCommand : IRequest<Unit>
{
    public int Id { get; set; }
}