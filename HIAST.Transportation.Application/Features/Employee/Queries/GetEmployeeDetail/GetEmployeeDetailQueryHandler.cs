using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Employee;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeDetail;

public class GetEmployeeDetailQueryHandler : IRequestHandler<GetEmployeeDetailQuery, EmployeeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetEmployeeDetailQueryHandler> _logger;

    public GetEmployeeDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetEmployeeDetailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching employee details for ID: {EmployeeId}", request.Id);

        var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.Id);
        if (employee == null)
        {
            _logger.LogWarning("Employee not found with ID: {EmployeeId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);
        }

        _logger.LogInformation("Successfully fetched employee details for ID: {EmployeeId}", request.Id);
        return _mapper.Map<EmployeeDto>(employee);
    }
}