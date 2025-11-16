using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class LineRepository : GenericRepository<Line>, ILineRepository
{
    public LineRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Line>> GetLinesBySupervisorIdAsync(int supervisorId)
    {
        return await _context.Lines
            .Where(l => l.SupervisorId == supervisorId)
            .Include(l => l.Supervisor)
            .ToListAsync();
    }

    public async Task<Line?> GetLineWithStopsAsync(int lineId)
    {
        return await _context.Lines
            .Include(l => l.LineStops)
            .ThenInclude(ls => ls.Stop)
            .Include(l => l.Supervisor)
            .FirstOrDefaultAsync(l => l.Id == lineId);
    }
}