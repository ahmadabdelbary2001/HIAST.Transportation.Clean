using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.LineSubscription;
using HIAST.Transportation.Application.Exceptions;
using MediatR;
using HIAST.Transportation.Application.Contracts.Identity;

namespace HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionDetail;

public class GetLineSubscriptionDetailQueryHandler : IRequestHandler<GetLineSubscriptionDetailQuery, LineSubscriptionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLineSubscriptionDetailQueryHandler> _logger;
    private readonly IUserService _userService;

    public GetLineSubscriptionDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetLineSubscriptionDetailQueryHandler> logger, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }

    public async Task<LineSubscriptionDto> Handle(GetLineSubscriptionDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching line subscription details for ID: {LineSubscriptionId}", request.Id);

        var lineSubscription = await _unitOfWork.LineSubscriptionRepository.GetLineSubscriptionWithDetailsAsync(request.Id);
        if (lineSubscription == null)
        {
            _logger.LogWarning("Line subscription not found with ID: {LineSubscriptionId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.LineSubscription), request.Id);
        }

        _logger.LogInformation("Successfully fetched line subscription details for ID: {LineSubscriptionId}", request.Id);
        var dto = _mapper.Map<LineSubscriptionDto>(lineSubscription);

        if (!string.IsNullOrEmpty(lineSubscription.EmployeeUserId))
        {
            var user = await _userService.GetEmployee(lineSubscription.EmployeeUserId);
            if (user != null)
            {
                dto.EmployeeName = $"{user.FirstName} {user.LastName}";
            }
        }

        return dto;
    }
}