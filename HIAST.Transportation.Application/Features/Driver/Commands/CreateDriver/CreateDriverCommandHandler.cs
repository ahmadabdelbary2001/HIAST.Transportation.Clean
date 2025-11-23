using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Driver.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateDriverCommandHandler> _logger;

    public CreateDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<CreateDriverCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting driver creation process");

        var validator = new CreateDriverDtoValidator();
        var validationResult = await validator.ValidateAsync(request.DriverDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Driver creation failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Driver", validationResult);
        }

        var driver = _mapper.Map<Domain.Entities.Driver>(request.DriverDto);
        
        _logger.LogInformation("Creating driver with name: {DriverName}", driver.Name);
        
        await _unitOfWork.DriverRepository.CreateAsync(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Driver created successfully with ID: {DriverId}", driver.Id);
        return driver.Id;
    }
}