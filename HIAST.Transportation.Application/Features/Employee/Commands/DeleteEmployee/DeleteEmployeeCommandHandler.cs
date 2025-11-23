using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<DeleteEmployeeCommandHandler> _logger;

    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteEmployeeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting employee deletion process for ID: {EmployeeId}", request.Id);

        var employeeToDelete = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.Id);
        if (employeeToDelete == null)
        {
            _logger.LogWarning("Employee not found for deletion with ID: {EmployeeId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Employee), request.Id);
        }

        _logger.LogInformation("Deleting employee with ID: {EmployeeId} and EmployeeNumber: {EmployeeNumber}", 
            employeeToDelete.Id, employeeToDelete.EmployeeNumber);

        await _unitOfWork.EmployeeRepository.DeleteAsync(employeeToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Employee deleted successfully with ID: {EmployeeId}", employeeToDelete.Id);
        return Unit.Value;
    }
}