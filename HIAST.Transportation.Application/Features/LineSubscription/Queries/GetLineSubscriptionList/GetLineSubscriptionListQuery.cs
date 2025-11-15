using HIAST.Transportation.Application.DTOs.LineSubscription;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionList;

public class GetLineSubscriptionListQuery : IRequest<List<LineSubscriptionListDto>>
{
}