using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Driver.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.UpdateDriver;

public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateDriverCommandHandler> _logger;

    public UpdateDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<UpdateDriverCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting driver update process for ID: {DriverId}", request.DriverDto.Id);

        var validator = new UpdateDriverDtoValidator();
        var validationResult = await validator.ValidateAsync(request.DriverDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Driver update failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Driver", validationResult);
        }

        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(request.DriverDto.Id);
        if (driver == null)
        {
            _logger.LogWarning("Driver not found with ID: {DriverId}", request.DriverDto.Id);
            throw new NotFoundException(nameof(Domain.Entities.Driver), request.DriverDto.Id);
        }

        _logger.LogInformation("Updating driver with ID: {DriverId}", driver.Id);
        
        _mapper.Map(request.DriverDto, driver);
        await _unitOfWork.DriverRepository.UpdateAsync(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Driver updated successfully with ID: {DriverId}", driver.Id);
        return Unit.Value;
    }
}