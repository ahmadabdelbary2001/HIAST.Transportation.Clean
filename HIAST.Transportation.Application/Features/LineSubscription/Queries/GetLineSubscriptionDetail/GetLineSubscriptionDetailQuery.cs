using HIAST.Transportation.Application.DTOs.LineSubscription;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionDetail;

public class GetLineSubscriptionDetailQuery : IRequest<LineSubscriptionDto>
{
    public int Id { get; set; }
}