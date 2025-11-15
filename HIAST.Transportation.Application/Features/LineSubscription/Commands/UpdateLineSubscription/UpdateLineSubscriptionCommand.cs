using HIAST.Transportation.Application.DTOs.LineSubscription;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.UpdateLineSubscription;

public class UpdateLineSubscriptionCommand : IRequest<Unit>
{
    public UpdateLineSubscriptionDto LineSubscriptionDto { get; set; } = null!;
}