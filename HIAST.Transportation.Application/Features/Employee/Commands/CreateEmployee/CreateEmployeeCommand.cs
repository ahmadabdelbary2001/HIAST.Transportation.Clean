using HIAST.Transportation.Application.DTOs.Employee;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<int>
{
    public CreateEmployeeDto EmployeeDto { get; set; } = null!;
}