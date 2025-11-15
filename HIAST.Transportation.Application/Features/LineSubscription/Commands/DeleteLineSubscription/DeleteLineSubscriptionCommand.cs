using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.DeleteLineSubscription;

public class DeleteLineSubscriptionCommand : IRequest<Unit>
{
    public int Id { get; set; }
}