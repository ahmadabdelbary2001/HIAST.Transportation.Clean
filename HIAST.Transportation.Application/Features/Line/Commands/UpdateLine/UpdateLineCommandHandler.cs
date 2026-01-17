using AutoMapper;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Application.DTOs.Line.Validators;
using HIAST.Transportation.Application.Exceptions;
using MediatR;
using HIAST.Transportation.Application.Contracts.Identity;

namespace HIAST.Transportation.Application.Features.Line.Commands.UpdateLine;

public class UpdateLineCommandHandler : IRequestHandler<UpdateLineCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateLineCommandHandler> _logger;
    private readonly Contracts.Infrastructure.INotificationService _notificationService;

    public UpdateLineCommandHandler(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper, IAppLogger<UpdateLineCommandHandler> logger, Contracts.Infrastructure.INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(UpdateLineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting line update process for ID: {LineId}", request.LineDto.Id);

        var validator = new UpdateLineDtoValidator(_unitOfWork.LineRepository);
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

        // Handle Bus Status Change
        if (line.BusId != request.LineDto.BusId)
        {
            // Set old bus to Available
            var oldBus = await _unitOfWork.BusRepository.GetByIdAsync(line.BusId);
            if (oldBus != null)
            {
                oldBus.Status = Domain.Enums.BusStatus.Available;
                await _unitOfWork.BusRepository.UpdateAsync(oldBus);
            }

            // Set new bus to InService
            var newBus = await _unitOfWork.BusRepository.GetByIdAsync(request.LineDto.BusId);
            if (newBus != null)
            {
                newBus.Status = Domain.Enums.BusStatus.InService;
                await _unitOfWork.BusRepository.UpdateAsync(newBus);
            }
        }

        // Handle Supervisor Change
        if (line.SupervisorId != request.LineDto.SupervisorId)
        {
            // Deactivate old supervisor's subscription
            var activeSubscriptions = await _unitOfWork.LineSubscriptionRepository.GetSubscriptionsByLineIdAsync(line.Id);
            
            // Notify active subscribers about supervisor change (Rule: If Admin changes, notify subscribers)
            var activeSubscribers = activeSubscriptions.Where(s => s.IsActive && s.EmployeeUserId != line.SupervisorId).ToList();
            if (activeSubscribers.Any())
            {
                var newSupervisor = await _userService.GetEmployee(request.LineDto.SupervisorId);
                var newSupervisorName = newSupervisor != null ? $"{newSupervisor.FirstName} {newSupervisor.LastName}" : "Unknown";

                var notificationTitle = "Supervisor Changed";
                var notificationMessage = $"The supervisor for line '{line.Name}' has been changed to '{newSupervisorName}'.";
                var notificationData = System.Text.Json.JsonSerializer.Serialize(new 
                { 
                    lineName = line.Name, 
                    newSupervisor = newSupervisorName 
                });
                
                foreach (var subscriber in activeSubscribers)
                {
                    await _notificationService.SendNotificationAsync(
                        subscriber.EmployeeUserId, 
                        notificationTitle, 
                        notificationMessage, 
                        line.Id.ToString(), 
                        "SupervisorChange",
                        "notifications.supervisorChanged.title",
                        "notifications.supervisorChanged.message",
                        notificationData
                    );
                }
            }

            var oldSubscription = activeSubscriptions.FirstOrDefault(s => s.EmployeeUserId == line.SupervisorId && s.IsActive);
            
            if (oldSubscription != null)
            {
                var subToUpdate = await _unitOfWork.LineSubscriptionRepository.GetByIdAsync(oldSubscription.Id);
                if (subToUpdate != null)
                {
                    subToUpdate.IsActive = false;
                    subToUpdate.EndDate = DateTime.UtcNow;
                    await _unitOfWork.LineSubscriptionRepository.UpdateAsync(subToUpdate);
                }
            }

            // Create subscription for new supervisor
            await _unitOfWork.LineSubscriptionRepository.CreateAsync(new Domain.Entities.LineSubscription
            {
                LineId = line.Id,
                EmployeeUserId = request.LineDto.SupervisorId,
                StartDate = DateTime.UtcNow,
                IsActive = true
            });
        }

        _logger.LogInformation("Updating line with ID: {LineId}", line.Id);
        
        _mapper.Map(request.LineDto, line);
        await _unitOfWork.LineRepository.UpdateAsync(line);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Line updated successfully with ID: {LineId}", line.Id);
        return Unit.Value;
    }
}
