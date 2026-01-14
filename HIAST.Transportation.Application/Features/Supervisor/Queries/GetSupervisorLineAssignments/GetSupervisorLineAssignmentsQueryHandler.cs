using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorLineAssignments;

public class GetSupervisorLineAssignmentsQueryHandler : IRequestHandler<GetSupervisorLineAssignmentsQuery, IReadOnlyList<SupervisorLineDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly Application.Contracts.Identity.IUserService _userService;

    public GetSupervisorLineAssignmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, Application.Contracts.Identity.IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<IReadOnlyList<SupervisorLineDto>> Handle(GetSupervisorLineAssignmentsQuery request, CancellationToken cancellationToken)
    {
        // 1. Fetch all lines with their supervisor details using the dedicated repository method.
        var lines = await _unitOfWork.LineRepository.GetAllLinesWithSupervisorDetailsAsync();

        var report = new List<SupervisorLineDto>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line.SupervisorId)) continue;
             
             var user = await _userService.GetEmployee(line.SupervisorId);
             if (user != null)
             {
                 report.Add(new SupervisorLineDto
                 {
                     EmployeeId = line.SupervisorId,
                     EmployeeNumber = user.EmployeeNumber ?? string.Empty,
                     EmployeeName = $"{user.Firstname} {user.Lastname}",
                     LineId = line.Id,
                     LineName = line.Name
                 });
             }
        }

        return report;
    }
}