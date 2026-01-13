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

    public async Task ReorderStopsAfterDeletionAsync(int lineId, int deletedSequenceOrder, bool wasTerminus)
    {
        var remainingStops = await _context.Stops
            .Where(s => s.LineId == lineId && s.SequenceOrder > deletedSequenceOrder)
            .OrderBy(s => s.SequenceOrder)
            .ToListAsync();

        // إعادة ترتيب المحطات التي بعد المحطة المحذوفة
        foreach (var stop in remainingStops)
        {
            stop.SequenceOrder--;
            _context.Stops.Update(stop);
        }

        // إذا كانت المحطة المحذوفة هي النهائية
        if (wasTerminus)
        {
            var newLastStop = await _context.Stops
                .Where(s => s.LineId == lineId)
                .OrderByDescending(s => s.SequenceOrder)
                .FirstOrDefaultAsync();

            if (newLastStop != null && newLastStop.StopType != Domain.Enums.StopType.Terminus)
            {
                newLastStop.StopType = Domain.Enums.StopType.Terminus;
                _context.Stops.Update(newLastStop);
            }
        }

        await _context.SaveChangesAsync();
    }
}