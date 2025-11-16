using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface ILineRepository : IGenericRepository<Line>
{
    Task<IReadOnlyList<Line>> GetLinesBySupervisorIdAsync(int supervisorId);
    Task<Line?> GetLineWithStopsAsync(int lineId);
}