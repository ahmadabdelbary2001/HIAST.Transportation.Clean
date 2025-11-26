using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Supervisor;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorLineAssignments;

public class GetSupervisorLineAssignmentsQueryHandler : IRequestHandler<GetSupervisorLineAssignmentsQuery, IReadOnlyList<SupervisorLineDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSupervisorLineAssignmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<SupervisorLineDto>> Handle(GetSupervisorLineAssignmentsQuery request, CancellationToken cancellationToken)
    {
        // 1. Fetch all lines with their supervisor details using the dedicated repository method.
        var lines = await _unitOfWork.LineRepository.GetAllLinesWithSupervisorDetailsAsync();

        // 2. Map the entities to the DTO.
        // We will perform the mapping manually here for clarity and to construct the EmployeeName.
        var report = lines.Select(line => new SupervisorLineDto
        {
            EmployeeId = line.Supervisor.Id,
            EmployeeNumber = line.Supervisor.EmployeeNumber,
            EmployeeName = $"{line.Supervisor.FirstName} {line.Supervisor.LastName}",
            LineId = line.Id,
            LineName = line.Name
        }).ToList();

        return report;
    }
}