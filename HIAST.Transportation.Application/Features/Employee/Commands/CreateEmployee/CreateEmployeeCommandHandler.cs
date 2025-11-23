using AutoMapper;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.DTOs.Employee.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateEmployeeCommandHandler> _logger;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<CreateEmployeeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting employee creation process");
        
        // 1. Validate the incoming DTO
        var validator = new CreateEmployeeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.EmployeeDto, cancellationToken);

        // 2. If validation fails, throw a custom exception
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Employee creation failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Employee", validationResult);
        }

        // 3. Map the DTO to the domain entity
        var employee = _mapper.Map<Domain.Entities.Employee>(request.EmployeeDto);

        _logger.LogInformation("Creating employee with name: {EmployeeName}", employee.FirstName);

        // 4. Add the new entity to the repository
        await _unitOfWork.EmployeeRepository.CreateAsync(employee);

        // 5. Save all changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Employee created successfully with ID: {EmployeeId}", employee.Id);

        // 6. Return the ID of the newly created employee
        return employee.Id;
    }
}