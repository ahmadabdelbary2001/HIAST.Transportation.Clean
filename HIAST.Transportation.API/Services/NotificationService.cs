using HIAST.Transportation.Application.Contracts.Infrastructure;
using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.API.Hubs;
using HIAST.Transportation.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace HIAST.Transportation.API.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IServiceProvider _serviceProvider;

    public NotificationService(IHubContext<NotificationHub> hubContext, IServiceProvider serviceProvider)
    {
        _hubContext = hubContext;
        _serviceProvider = serviceProvider;
    }

    public async Task SendNotificationAsync(
        string userId, 
        string title, 
        string message, 
        string? relatedEntityId = null, 
        string? type = null, 
        string? titleKey = null, 
        string? messageKey = null, 
        string? data = null)
    {
        // 1. Save to Database
        using (var scope = _serviceProvider.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var notification = new Notification
            {
                Title = title,
                Message = message,
                TitleKey = titleKey,
                MessageKey = messageKey,
                Data = data,
                UserId = userId,
                RelatedEntityId = relatedEntityId,
                Type = type,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.NotificationRepository.CreateAsync(notification);
            await unitOfWork.SaveChangesAsync();
        
            // 2. Send via SignalR
            await _hubContext.Clients.Group(userId).SendAsync("ReceiveNotification", notification);
        }
    }
}
