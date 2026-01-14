using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Line;
using HIAST.Transportation.Application.Exceptions;
using MediatR;
using HIAST.Transportation.Application.Contracts.Identity;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineDetail;

public class GetLineDetailQueryHandler : IRequestHandler<GetLineDetailQuery, LineDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLineDetailQueryHandler> _logger;
    private readonly IUserService _userService;

    public GetLineDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetLineDetailQueryHandler> logger, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }

    public async Task<LineDto> Handle(GetLineDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching line details with stops for ID: {LineId}", request.Id);

        var line = await _unitOfWork.LineRepository.GetLineWithDetailsAsync(request.Id);
        if (line == null)
        {
            _logger.LogWarning("Line not found with ID: {LineId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Line), request.Id);
        }

        var dto = _mapper.Map<LineDto>(line);

        // Hydrate Supervisor Name
        if (!string.IsNullOrEmpty(line.SupervisorId))
        {
            var user = await _userService.GetEmployee(line.SupervisorId);
            if (user != null)
            {
                dto.SupervisorName = $"{user.Firstname} {user.Lastname}";
            }
        }

        // Hydrate Subscription Employee Names
        foreach (var sub in dto.Subscriptions)
        {
             var subEntity = line.LineSubscriptions.FirstOrDefault(ls => ls.Id == sub.Id);
             if (subEntity != null && !string.IsNullOrEmpty(subEntity.EmployeeUserId))
             {
                 var user = await _userService.GetEmployee(subEntity.EmployeeUserId);
                 if (user != null)
                 {
                     sub.EmployeeName = $"{user.Firstname} {user.Lastname}";
                 }
             }
        }

        _logger.LogInformation("Successfully fetched line details with {StopCount} stops for ID: {LineId}", 
            line.Stops?.Count ?? 0, request.Id);
        return dto;
    }
}