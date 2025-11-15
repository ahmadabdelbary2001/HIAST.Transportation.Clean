using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Employee.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // 1. Validate the incoming DTO
        var validator = new CreateEmployeeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.EmployeeDto, cancellationToken);

        // 2. If validation fails, throw a custom exception
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }

        // 3. Map the DTO to the domain entity
        var employee = _mapper.Map<Domain.Entities.Employee>(request.EmployeeDto);

        // 4. Add the new entity to the repository
        await _unitOfWork.EmployeeRepository.CreateAsync(employee);

        // 5. Save all changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 6. Return the ID of the newly created employee
        return employee.Id;
    }
}