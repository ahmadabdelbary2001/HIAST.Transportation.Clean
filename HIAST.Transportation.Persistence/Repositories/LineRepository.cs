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
}