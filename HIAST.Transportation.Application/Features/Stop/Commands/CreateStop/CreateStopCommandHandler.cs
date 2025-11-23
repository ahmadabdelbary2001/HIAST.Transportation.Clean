using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Stop.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Stop.Commands.CreateStop;

public class CreateStopCommandHandler : IRequestHandler<CreateStopCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateStopCommandHandler> _logger;

    public CreateStopCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<CreateStopCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateStopCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting stop creation process");

        var validator = new CreateStopDtoValidator();
        var validationResult = await validator.ValidateAsync(request.StopDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Stop creation failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Stop", validationResult);
        }

        var stop = _mapper.Map<Domain.Entities.Stop>(request.StopDto);
        
        _logger.LogInformation("Creating stop with address: {Address}", stop.Address);

        await _unitOfWork.StopRepository.CreateAsync(stop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Stop created successfully with ID: {StopId}", stop.Id);
        return stop.Id;
    }
}