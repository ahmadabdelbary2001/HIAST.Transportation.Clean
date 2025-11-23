using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Bus;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Bus.Queries.GetBusDetail;

public class GetBusDetailQueryHandler : IRequestHandler<GetBusDetailQuery, BusDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetBusDetailQueryHandler> _logger;

    public GetBusDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetBusDetailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BusDto> Handle(GetBusDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching bus details for ID: {BusId}", request.Id);

        var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.Id);
        if (bus == null)
        {
            _logger.LogWarning("Bus not found with ID: {BusId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Bus), request.Id);
        }

        _logger.LogInformation("Successfully fetched bus details for ID: {BusId}", request.Id);
        return _mapper.Map<BusDto>(bus);
    }
}