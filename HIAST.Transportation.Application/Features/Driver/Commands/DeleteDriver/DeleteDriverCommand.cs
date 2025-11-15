using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.DeleteDriver;

public class DeleteDriverCommand : IRequest<Unit>
{
    public int Id { get; set; }
}