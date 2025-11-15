using HIAST.Transportation.Application.DTOs.LineSubscription;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Commands.CreateLineSubscription;

public class CreateLineSubscriptionCommand : IRequest<int>
{
    public CreateLineSubscriptionDto LineSubscriptionDto { get; set; } = null!;
}