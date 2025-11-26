using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface IStopRepository : IGenericRepository<Stop>
{
    Task<IReadOnlyList<Stop>> GetStopsByLineIdAsync(int lineId);
    Task<IReadOnlyList<Stop>> GetAllStopsWithDetailsAsync();
    Task<Stop?> GetStopWithDetailsAsync(int id);
}