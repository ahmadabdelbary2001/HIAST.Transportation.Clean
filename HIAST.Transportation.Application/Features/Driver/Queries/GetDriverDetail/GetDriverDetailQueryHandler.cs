using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Driver;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Driver.Queries.GetDriverDetail;

public class GetDriverDetailQueryHandler : IRequestHandler<GetDriverDetailQuery, DriverDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetDriverDetailQueryHandler> _logger;

    public GetDriverDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetDriverDetailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<DriverDto> Handle(GetDriverDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching driver details for ID: {DriverId}", request.Id);

        var driver = await _unitOfWork.DriverRepository.GetDriverWithDetailsAsync(request.Id);
        if (driver == null)
        {
            _logger.LogWarning("Driver not found with ID: {DriverId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Driver), request.Id);
        }

        var driverDto = _mapper.Map<DriverDto>(driver);

        _logger.LogInformation("Successfully fetched driver details for ID: {DriverId}", request.Id);
        return driverDto;
    }
}