using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Line.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;

namespace HIAST.Transportation.Application.Features.Line.Commands.UpdateLine;

public class UpdateLineCommandHandler : IRequestHandler<UpdateLineCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateLineCommandHandler> _logger;

    public UpdateLineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<UpdateLineCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting line update process for ID: {LineId}", request.LineDto.Id);

        var validator = new UpdateLineDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LineDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Line update failed validation: {Errors}", validationResult.Errors);
            throw new BadRequestException("Invalid Line", validationResult);
        }

        var line = await _unitOfWork.LineRepository.GetByIdAsync(request.LineDto.Id);
        if (line == null)
        {
            _logger.LogWarning("Line not found with ID: {LineId}", request.LineDto.Id);
            throw new NotFoundException(nameof(Domain.Entities.Line), request.LineDto.Id);
        }

        _logger.LogInformation("Updating line with ID: {LineId}", line.Id);
        
        _mapper.Map(request.LineDto, line);
        await _unitOfWork.LineRepository.UpdateAsync(line);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Line updated successfully with ID: {LineId}", line.Id);
        return Unit.Value;
    }
}