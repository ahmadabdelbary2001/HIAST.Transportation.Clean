using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Stop;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Queries.GetStopDetail;

public class GetStopDetailQueryHandler : IRequestHandler<GetStopDetailQuery, StopDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetStopDetailQueryHandler> _logger;

    public GetStopDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetStopDetailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<StopDto> Handle(GetStopDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching stop details for ID: {StopId}", request.Id);

        var stop = await _unitOfWork.StopRepository.GetStopWithDetailsAsync(request.Id);
        if (stop == null)
        {
            _logger.LogWarning("Stop not found with ID: {StopId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Stop), request.Id);
        }

        _logger.LogInformation("Successfully fetched stop details for ID: {StopId}", request.Id);
        return _mapper.Map<StopDto>(stop);
    }
}