using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Line;
using MediatR;
using HIAST.Transportation.Application.Contracts.Identity;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineList;

public class GetLineListQueryHandler : IRequestHandler<GetLineListQuery, List<LineListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLineListQueryHandler> _logger;
    private readonly IUserService _userService;

    public GetLineListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetLineListQueryHandler> logger, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }

    public async Task<List<LineListDto>> Handle(GetLineListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all lines");

        var lines = await _unitOfWork.LineRepository.GetAllLinesWithSupervisorDetailsAsync();
        
        var dtos = _mapper.Map<List<LineListDto>>(lines);

        // Manually hydrate Supervisor Names from Identity
        foreach (var dto in dtos)
        {
            var line = lines.FirstOrDefault(l => l.Id == dto.Id);
            if (line != null && !string.IsNullOrEmpty(line.SupervisorId))
            {
                var user = await _userService.GetEmployee(line.SupervisorId);
                if (user != null)
                {
                    dto.SupervisorName = $"{user.FirstName} {user.LastName}";
                }
            }
        }

        _logger.LogInformation("Successfully fetched {LineCount} lines", lines.Count);
        return dtos;
    }
}