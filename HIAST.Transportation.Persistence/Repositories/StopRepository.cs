using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class StopRepository : GenericRepository<Stop>, IStopRepository
{
    public StopRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Stop>> GetStopsByLineIdAsync(int lineId)
    {
        return await _context.LineStops
            .Where(ls => ls.LineId == lineId)
            .OrderBy(ls => ls.SequenceOrder)
            .Select(ls => ls.Stop)
            .ToListAsync();
    }
}