using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Employee;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeList;

public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetEmployeeListQueryHandler> _logger;

    public GetEmployeeListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetEmployeeListQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<EmployeeListDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching list of all employees");

        var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
        
        _logger.LogInformation("Successfully fetched {EmployeeCount} employees", employees.Count);
        return _mapper.Map<List<EmployeeListDto>>(employees);
    }
}