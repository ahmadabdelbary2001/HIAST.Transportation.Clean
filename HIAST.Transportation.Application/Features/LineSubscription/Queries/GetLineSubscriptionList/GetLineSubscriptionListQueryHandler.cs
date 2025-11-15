using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.LineSubscription;
using MediatR;

namespace HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionList;

public class GetLineSubscriptionListQueryHandler : IRequestHandler<GetLineSubscriptionListQuery, List<LineSubscriptionListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLineSubscriptionListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<LineSubscriptionListDto>> Handle(GetLineSubscriptionListQuery request, CancellationToken cancellationToken)
    {
        var lineSubscriptions = await _unitOfWork.LineSubscriptionRepository.GetAllAsync();
        return _mapper.Map<List<LineSubscriptionListDto>>(lineSubscriptions);
    }
}