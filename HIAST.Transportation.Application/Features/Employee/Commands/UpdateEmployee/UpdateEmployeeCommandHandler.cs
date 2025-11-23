using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Employee.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // 1. Validate the incoming DTO data.
        var validator = new UpdateEmployeeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.EmployeeDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid Employee", validationResult);

        // 2. Retrieve the existing employee from the database.
        var employeeToUpdate = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.EmployeeDto.Id);

        // 3. If the employee doesn't exist, throw a specific exception.
        if (employeeToUpdate == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Employee), request.EmployeeDto.Id);
        }

        // 4. Map the DTO onto the existing entity. AutoMapper will update the properties.
        _mapper.Map(request.EmployeeDto, employeeToUpdate);

        // 5. Tell the repository that the entity has been updated.
        await _unitOfWork.EmployeeRepository.UpdateAsync(employeeToUpdate);

        // 6. Save all changes to the database.
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 7. Return Unit.Value to signify completion.
        return Unit.Value;
    }
}
