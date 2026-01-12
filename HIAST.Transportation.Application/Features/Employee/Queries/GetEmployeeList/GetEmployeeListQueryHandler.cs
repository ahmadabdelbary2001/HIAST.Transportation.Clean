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
        
        var employeeDtos = _mapper.Map<List<EmployeeListDto>>(employees);

        foreach (var dto in employeeDtos)
        {
            var isSupervisor = await _unitOfWork.LineRepository.IsSupervisorAssignedAsync(dto.Id);
            var isSubscribed = await _unitOfWork.LineSubscriptionRepository.IsEmployeeSubscribedAsync(dto.Id);
            dto.IsAssigned = isSupervisor || isSubscribed;
            dto.HasSubscription = await _unitOfWork.LineSubscriptionRepository.HasAnySubscriptionAsync(dto.Id);
        }

        _logger.LogInformation("Successfully fetched {EmployeeCount} employees", employees.Count);
        return employeeDtos;
    }
}