using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        // 1. Find the employee to be deleted.
        var employeeToDelete = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.Id);

        // 2. If the employee doesn't exist, throw a specific exception.
        if (employeeToDelete == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);
        }

        // 3. Delete the entity from the repository.
        await _unitOfWork.EmployeeRepository.DeleteAsync(employeeToDelete);

        // 4. Save all changes to the database.
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 5. Return a Unit.Value to signify completion.
        return Unit.Value;
    }
}