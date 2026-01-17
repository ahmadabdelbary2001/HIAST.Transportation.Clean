using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Infrastructure;

public interface INotificationService
{
    Task SendNotificationAsync(
        string userId, 
        string title, 
        string message, 
        string? relatedEntityId = null, 
        string? type = null, 
        string? titleKey = null, 
        string? messageKey = null, 
        string? data = null);
}
