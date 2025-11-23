// UpdateEmployeeCommandHandler.cs
using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Employee.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateEmployeeCommandHandler> _logger;

    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<UpdateEmployeeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting employee update process for ID: {EmployeeId}", request.EmployeeDto.Id);

        var validator = new UpdateEmployeeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.EmployeeDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Employee update failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Employee", validationResult);
        }

        var employeeToUpdate = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.EmployeeDto.Id);
        if (employeeToUpdate == null)
        {
            _logger.LogWarning("Employee not found with ID: {EmployeeId}", request.EmployeeDto.Id);
            throw new NotFoundException(nameof(Domain.Entities.Employee), request.EmployeeDto.Id);
        }

        _logger.LogInformation("Updating employee with ID: {EmployeeId}", employeeToUpdate.Id);
        
        _mapper.Map(request.EmployeeDto, employeeToUpdate);
        await _unitOfWork.EmployeeRepository.UpdateAsync(employeeToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Employee updated successfully with ID: {EmployeeId}", employeeToUpdate.Id);
        return Unit.Value;
    }
}