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
        return await _context.Stops
            .Where(s => s.LineId == lineId)
            .Include(s => s.Line) // Eagerly load Line for DTO mapping
            .OrderBy(s => s.SequenceOrder)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IReadOnlyList<Stop>> GetAllStopsWithDetailsAsync()
    {
        return await _context.Stops
            .Include(s => s.Line)
            .OrderBy(s => s.Line != null ? s.Line.Name : string.Empty) 
            .ThenBy(s => s.SequenceOrder)
            .AsNoTracking()
            .ToListAsync();    }
    public async Task<Stop?> GetStopWithDetailsAsync(int id)
    {
        return await _context.Stops
            .Include(s => s.Line) // Eagerly load the related Line
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}