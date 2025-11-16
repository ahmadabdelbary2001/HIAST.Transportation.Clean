using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface ITripRepository : IGenericRepository<Trip>
{
    Task<IReadOnlyList<Trip>> GetTripsByLineIdAsync(int lineId);
    Task<IReadOnlyList<Trip>> GetTripsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IReadOnlyList<Trip>> GetTripsByStatusAsync(TripStatus status);
}