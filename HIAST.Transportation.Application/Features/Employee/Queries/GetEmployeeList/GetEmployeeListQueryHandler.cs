using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Employee;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeList;

public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmployeeListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<EmployeeListDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
        
        return _mapper.Map<List<EmployeeListDto>>(employees);
    }
}