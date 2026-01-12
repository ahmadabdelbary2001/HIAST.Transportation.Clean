using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface ILineSubscriptionRepository : IGenericRepository<LineSubscription>
{
    Task<IReadOnlyList<LineSubscription>> GetSubscriptionsByEmployeeIdAsync(int employeeId);
    Task<IReadOnlyList<LineSubscription>> GetSubscriptionsByLineIdAsync(int lineId);
    Task<IReadOnlyList<LineSubscription>> GetActiveSubscriptionsAsync();
    Task<LineSubscription?> GetLineSubscriptionWithDetailsAsync(int id);
    Task<IReadOnlyList<LineSubscription>> GetAllLineSubscriptionsWithDetailsAsync();
    Task<bool> IsEmployeeSubscribedAsync(int employeeId);
    Task<bool> HasAnySubscriptionAsync(int employeeId);
}