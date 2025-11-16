using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class LineStopRepository : GenericRepository<LineStop>, ILineStopRepository
{
    public LineStopRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<LineStop>> GetStopsByLineIdOrderedAsync(int lineId)
    {
        return await _context.LineStops
            .Where(ls => ls.LineId == lineId)
            .Include(ls => ls.Stop)
            .Include(ls => ls.Line)
            .OrderBy(ls => ls.SequenceOrder)
            .ToListAsync();
    }
}