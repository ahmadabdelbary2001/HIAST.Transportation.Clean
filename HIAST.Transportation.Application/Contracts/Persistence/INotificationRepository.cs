using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<List<Notification>> GetUnreadNotificationsByUserIdAsync(string userId);
    Task<List<Notification>> GetAllNotificationsByUserIdAsync(string userId);
}
