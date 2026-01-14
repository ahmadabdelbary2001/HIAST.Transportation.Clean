using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface ILineRepository : IGenericRepository<Line>
{
    Task<IReadOnlyList<Line>> GetLinesBySupervisorIdAsync(string supervisorId);
    Task<Line?> GetLineWithDetailsAsync(int lineId);
    Task<IReadOnlyList<Line>> GetAllLinesWithSupervisorDetailsAsync();
    
    Task<bool> IsBusAssignedAsync(int busId, int? excludeLineId = null);
    Task<bool> IsDriverAssignedAsync(int driverId, int? excludeLineId = null);
    Task<bool> IsSupervisorAssignedAsync(string supervisorId, int? excludeLineId = null);

    Task<Line?> GetLineByBusIdAsync(int busId);
}