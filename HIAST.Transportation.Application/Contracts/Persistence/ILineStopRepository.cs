using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface ILineStopRepository : IGenericRepository<LineStop>
{
    Task<IReadOnlyList<LineStop>> GetStopsByLineIdOrderedAsync(int lineId);
}