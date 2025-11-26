using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Line;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Queries.GetLineDetail;

public class GetLineDetailQueryHandler : IRequestHandler<GetLineDetailQuery, LineDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLineDetailQueryHandler> _logger;

    public GetLineDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetLineDetailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LineDto> Handle(GetLineDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching line details with stops for ID: {LineId}", request.Id);

        var line = await _unitOfWork.LineRepository.GetLineWithDetailsAsync(request.Id);
        if (line == null)
        {
            _logger.LogWarning("Line not found with ID: {LineId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Line), request.Id);
        }

        _logger.LogInformation("Successfully fetched line details with {StopCount} stops for ID: {LineId}", 
            line.Stops?.Count ?? 0, request.Id);
        return _mapper.Map<LineDto>(line);
    }
}