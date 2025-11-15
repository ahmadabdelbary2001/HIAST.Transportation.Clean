using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Commands.DeleteBus;

public class DeleteBusCommand : IRequest<Unit>
{
    public int Id { get; set; }
}