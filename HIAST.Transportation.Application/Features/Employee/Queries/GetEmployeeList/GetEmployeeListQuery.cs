using HIAST.Transportation.Application.DTOs.Employee;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeList;

public class GetEmployeeListQuery : IRequest<List<EmployeeListDto>>
{
}