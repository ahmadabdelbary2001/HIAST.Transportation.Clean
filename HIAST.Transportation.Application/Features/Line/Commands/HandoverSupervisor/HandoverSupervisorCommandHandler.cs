using AutoMapper;
using MediatR;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Application.Exceptions;

namespace HIAST.Transportation.Application.Features.Line.Commands.HandoverSupervisor;

public class HandoverSupervisorCommandHandler : IRequestHandler<HandoverSupervisorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly Contracts.Infrastructure.INotificationService _notificationService;

    public HandoverSupervisorCommandHandler(IUnitOfWork unitOfWork, IUserService userService, Contracts.Infrastructure.INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _notificationService = notificationService;
    }

    public async Task Handle(HandoverSupervisorCommand request, CancellationToken cancellationToken)
    {
        // 1. Get Line for verification and update
        var line = await _unitOfWork.LineRepository.GetByIdAsync(request.HandoverSupervisorDto.LineId);
        if (line == null)
            throw new NotFoundException(nameof(Domain.Entities.Line), request.HandoverSupervisorDto.LineId);

        var currentUserId = _userService.UserId;

        // 2. Verify Current Supervisor
        if (line.SupervisorId != currentUserId)
            throw new BadRequestException("Only the current supervisor can handover the line.");

        // 3. Get subscriptions WITHOUT including Line navigation (to avoid tracking conflict)
        var subscriptions = await _unitOfWork.LineSubscriptionRepository.GetSubscriptionsByLineIdAsync(line.Id);
        
        // 4. Verify New Supervisor is Subscribed
        var newSupervisorSubscription = subscriptions.FirstOrDefault(s => s.EmployeeUserId == request.HandoverSupervisorDto.NewSupervisorId && s.IsActive);

        if (newSupervisorSubscription == null)
            throw new BadRequestException("Selected new supervisor is not an active subscriber of this line.");

        // Fetch names for notification
        var oldSupervisor = await _userService.GetEmployee(currentUserId);
        var newSupervisor = await _userService.GetEmployee(request.HandoverSupervisorDto.NewSupervisorId);
        var oldSupervisorName = oldSupervisor != null ? $"{oldSupervisor.FirstName} {oldSupervisor.LastName}" : "Unknown";
        var newSupervisorName = newSupervisor != null ? $"{newSupervisor.FirstName} {newSupervisor.LastName}" : "Unknown";

        // 5. Update Supervisor
        line.SupervisorId = request.HandoverSupervisorDto.NewSupervisorId;
        await _unitOfWork.LineRepository.UpdateAsync(line);
        
        // Save changes to update the line
        await _unitOfWork.SaveChangesAsync();

        // 6. Notifications
        var notificationTitle = "Supervisor Handover";
        var notificationMessage = $"Supervisor handover for line '{line.Name}': {oldSupervisorName} -> {newSupervisorName}.";
        var adminData = System.Text.Json.JsonSerializer.Serialize(new 
        { 
            lineName = line.Name, 
            oldSupervisor = oldSupervisorName,
            newSupervisor = newSupervisorName 
        });

        // Notify Admins (Rule: If Supervisor handovers, notify Admins)
        var adminIds = await _userService.GetAdminUserIdsAsync();
        foreach (var adminId in adminIds)
        {
             await _notificationService.SendNotificationAsync(
                adminId,
                notificationTitle,
                notificationMessage,
                line.Id.ToString(),
                "SupervisorHandover",
                "notifications.supervisorHandover.title",
                "notifications.supervisorHandover.message",
                adminData
            );
        }

        // Notify Subscribers (Implicit Rule: Subscribers should know)
        // Filter out the old supervisor (who is leaving) and maybe active subscribers
        var activeSubscribers = subscriptions.Where(s => s.IsActive && s.EmployeeUserId != currentUserId).ToList();
        var subscriberData = System.Text.Json.JsonSerializer.Serialize(new 
        { 
            lineName = line.Name, 
            newSupervisor = newSupervisorName 
        });
        
        foreach (var sub in activeSubscribers)
        {
            await _notificationService.SendNotificationAsync(
                sub.EmployeeUserId, 
                "Supervisor Changed", 
                $"The supervisor for line '{line.Name}' has been changed to '{newSupervisorName}'.",
                line.Id.ToString(), 
                "SupervisorChange",
                "notifications.supervisorChanged.title",
                "notifications.supervisorChanged.message",
                subscriberData
            );
        }

        // 7. Find and Delete Old Supervisor's Subscription
        var oldSupervisorSubscriptionId = subscriptions
            .FirstOrDefault(s => s.EmployeeUserId == currentUserId && s.IsActive)?.Id;
        
        if (oldSupervisorSubscriptionId.HasValue)
        {
            // Get the subscription by ID (fresh from DB without tracking conflicts)
            var oldSubscription = await _unitOfWork.LineSubscriptionRepository.GetByIdAsync(oldSupervisorSubscriptionId.Value);
            if (oldSubscription != null)
            {
                await _unitOfWork.LineSubscriptionRepository.DeleteAsync(oldSubscription);
                // Save again to delete the subscription
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
