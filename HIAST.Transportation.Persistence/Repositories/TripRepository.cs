using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Domain.Enums;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class TripRepository : GenericRepository<Trip>, ITripRepository
{
    public TripRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Trip>> GetTripsByLineIdAsync(int lineId)
    {
        return await _context.Trips
            .Where(t => t.LineId == lineId)
            .Include(t => t.Line)
            .Include(t => t.Bus)
            .Include(t => t.Driver)
            .OrderBy(t => t.ScheduledTime)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Trip>> GetTripsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Trips
            .Where(t => t.ScheduledTime >= startDate && t.ScheduledTime <= endDate)
            .Include(t => t.Line)
            .Include(t => t.Bus)
            .Include(t => t.Driver)
            .OrderBy(t => t.ScheduledTime)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Trip>> GetTripsByStatusAsync(TripStatus status)
    {
        return await _context.Trips
            .Where(t => t.Status == status)
            .Include(t => t.Line)
            .Include(t => t.Bus)
            .Include(t => t.Driver)
            .OrderBy(t => t.ScheduledTime)
            .ToListAsync();
    }
}