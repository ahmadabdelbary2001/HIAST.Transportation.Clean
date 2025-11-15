using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.DeleteStop;

public class DeleteStopCommand : IRequest<Unit>
{
    public int Id { get; set; }
}