using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.LineSubscription;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionList;

public class GetLineSubscriptionListQueryHandler : IRequestHandler<GetLineSubscriptionListQuery, List<LineSubscriptionListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLineSubscriptionListQueryHandler> _logger;

    public GetLineSubscriptionListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetLineSubscriptionListQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LineSubscriptionListDto>> Handle(GetLineSubscriptionListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all line subscriptions");

        var lineSubscriptions = await _unitOfWork.LineSubscriptionRepository.GetAllLineSubscriptionsWithDetailsAsync();
        
        _logger.LogInformation("Successfully fetched {LineSubscriptionCount} line subscriptions", lineSubscriptions.Count);
        return _mapper.Map<List<LineSubscriptionListDto>>(lineSubscriptions);
    }
}