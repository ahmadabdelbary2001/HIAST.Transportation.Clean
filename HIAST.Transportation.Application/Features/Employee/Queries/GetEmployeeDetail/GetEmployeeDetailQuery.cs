using HIAST.Transportation.Application.DTOs.Employee;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeDetail;

public class GetEmployeeDetailQuery : IRequest<EmployeeDto>
{
    public int Id { get; set; }
}