using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Employee;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeDetail;

public class GetEmployeeDetailQueryHandler : IRequestHandler<GetEmployeeDetailQuery, EmployeeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployeeDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
    {
        // In IUnitOfWork, the property should be EmployeeRepository
        var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.Id);

        if (employee == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);
        }

        return _mapper.Map<EmployeeDto>(employee);
    }
}