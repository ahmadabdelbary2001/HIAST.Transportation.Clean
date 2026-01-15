using MediatR;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Application.Models.Dashboard;

namespace HIAST.Transportation.Application.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQuery : IRequest<DashboardStatsDto>
{
}

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly IBusRepository _busRepository;
    private readonly IUserService _userService;
    private readonly ILineRepository _lineRepository;
    private readonly ILineSubscriptionRepository _subscriptionRepository;

    public GetDashboardStatsQueryHandler(
        IBusRepository busRepository,
        IUserService userService,
        ILineRepository lineRepository,
        ILineSubscriptionRepository subscriptionRepository)
    {
        _busRepository = busRepository;
        _userService = userService;
        _lineRepository = lineRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var activeBuses = await _busRepository.CountAsync();
        var totalEmployees = await _userService.GetEmployeeCountAsync();
        var totalLines = await _lineRepository.CountAsync();
        var totalSubscriptions = await _subscriptionRepository.CountAsync();

        return new DashboardStatsDto
        {
            ActiveBuses = activeBuses,
            TotalEmployees = totalEmployees,
            TotalLines = totalLines,
            TotalSubscriptions = totalSubscriptions
        };
    }
}
