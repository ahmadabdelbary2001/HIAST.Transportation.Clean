using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Supervisor;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorDetail;

public class GetSupervisorDetailQueryHandler : IRequestHandler<GetSupervisorDetailQuery, SupervisorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetSupervisorDetailQueryHandler> _logger;

    public GetSupervisorDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<GetSupervisorDetailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SupervisorDto> Handle(GetSupervisorDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching supervisor details for ID: {SupervisorId}", request.Id);

        var supervisor = await _unitOfWork.SupervisorRepository.GetByIdAsync(request.Id);
        if (supervisor == null)
        {
            _logger.LogWarning("Supervisor not found with ID: {SupervisorId}", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.Supervisor), request.Id);
        }

        _logger.LogInformation("Successfully fetched supervisor details for ID: {SupervisorId}", request.Id);
        return _mapper.Map<SupervisorDto>(supervisor);
    }
}