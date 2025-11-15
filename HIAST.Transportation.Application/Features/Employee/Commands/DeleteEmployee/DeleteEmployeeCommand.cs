using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}