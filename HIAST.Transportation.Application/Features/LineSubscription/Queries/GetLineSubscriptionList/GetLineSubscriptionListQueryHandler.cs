using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.LineSubscription;
using MediatR;
using HIAST.Transportation.Application.Contracts.Identity;

namespace HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionList;

public class GetLineSubscriptionListQueryHandler : IRequestHandler<GetLineSubscriptionListQuery, List<LineSubscriptionListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLineSubscriptionListQueryHandler> _logger;
    private readonly Application.Contracts.Identity.IUserService _userService;

    public GetLineSubscriptionListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetLineSubscriptionListQueryHandler> logger, Application.Contracts.Identity.IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }

    public async Task<List<LineSubscriptionListDto>> Handle(GetLineSubscriptionListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all line subscriptions");

        var lineSubscriptions = await _unitOfWork.LineSubscriptionRepository.GetAllLineSubscriptionsWithDetailsAsync();
        
        var dtos = _mapper.Map<List<LineSubscriptionListDto>>(lineSubscriptions);

        foreach (var dto in dtos)
        {
            var sub = lineSubscriptions.FirstOrDefault(ls => ls.Id == dto.Id);
            if (sub != null && !string.IsNullOrEmpty(sub.EmployeeUserId))
            {
                var user = await _userService.GetEmployee(sub.EmployeeUserId);
                if (user != null)
                {
                    dto.EmployeeName = $"{user.Firstname} {user.Lastname}";
                }
            }
        }

        _logger.LogInformation("Successfully fetched {LineSubscriptionCount} line subscriptions", lineSubscriptions.Count);
        return dtos;
    }
}