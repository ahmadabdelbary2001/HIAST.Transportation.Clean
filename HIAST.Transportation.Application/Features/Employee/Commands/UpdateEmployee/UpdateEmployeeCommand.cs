using HIAST.Transportation.Application.DTOs.Employee;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<Unit>
{
    public UpdateEmployeeDto EmployeeDto { get; set; } = null!;
}