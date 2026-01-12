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
            .Include(l => l.Bus)
            .Include(l => l.Driver)
            .ToListAsync();
    }

    public async Task<Line?> GetLineWithDetailsAsync(int lineId) // Renamed for clarity
    {
        return await _context.Lines
            .Include(l => l.Supervisor)
            .Include(l => l.Bus)
            .Include(l => l.Driver)
            .Include(l => l.Stops.OrderBy(s => s.SequenceOrder))
            .Include(l => l.LineSubscriptions)
            .ThenInclude(ls => ls.Employee) // Essential for getting the EmployeeName
            .FirstOrDefaultAsync(l => l.Id == lineId);
    }
    
    public async Task<IReadOnlyList<Line>> GetAllLinesWithSupervisorDetailsAsync()
    {
        return await _context.Lines
            .Include(l => l.Supervisor) // Eagerly load the Supervisor navigation property.
            .AsNoTracking()             // Use for read-only queries to boost performance.
            .ToListAsync();
    }

    public async Task<bool> IsBusAssignedAsync(int busId, int? excludeLineId = null)
    {
        var query = _context.Lines.AsQueryable();
        if (excludeLineId.HasValue)
        {
            query = query.Where(l => l.Id != excludeLineId.Value);
        }
        return await query.AnyAsync(l => l.BusId == busId);
    }

    public async Task<bool> IsDriverAssignedAsync(int driverId, int? excludeLineId = null)
    {
        var query = _context.Lines.AsQueryable();
        if (excludeLineId.HasValue)
        {
            query = query.Where(l => l.Id != excludeLineId.Value);
        }
        return await query.AnyAsync(l => l.DriverId == driverId);
    }

    public async Task<bool> IsSupervisorAssignedAsync(int supervisorId, int? excludeLineId = null)
    {
        var query = _context.Lines.AsQueryable();
        if (excludeLineId.HasValue)
        {
            query = query.Where(l => l.Id != excludeLineId.Value);
        }
        return await query.AnyAsync(l => l.SupervisorId == supervisorId);
    }

    public async Task<Line?> GetLineByBusIdAsync(int busId)
    {
        return await _context.Lines.FirstOrDefaultAsync(l => l.BusId == busId);
    }
}