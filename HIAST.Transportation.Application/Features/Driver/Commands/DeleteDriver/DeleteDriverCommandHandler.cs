using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Commands.DeleteDriver;

public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppLogger<DeleteDriverCommandHandler> _logger;

    public DeleteDriverCommandHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteDriverCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting driver deletion process for ID: {DriverId}", request.Id);

        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(request.Id);
        if (driver == null)
        {
            _logger.LogWarning("Driver not found for deletion with ID: {DriverId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Driver), request.Id);
        }

        _logger.LogInformation("Deleting driver with ID: {DriverId} and name: {DriverName}", 
            driver.Id, driver.Name);

        await _unitOfWork.DriverRepository.DeleteAsync(driver);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Driver deleted successfully with ID: {DriverId}", driver.Id);
        return Unit.Value;
    }
}