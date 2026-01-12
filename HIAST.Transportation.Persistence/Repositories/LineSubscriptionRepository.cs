using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class LineSubscriptionRepository : GenericRepository<LineSubscription>, ILineSubscriptionRepository
{
    public LineSubscriptionRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<LineSubscription>> GetSubscriptionsByEmployeeIdAsync(int employeeId)
    {
        return await _context.LineSubscriptions
            .Where(ls => ls.EmployeeId == employeeId)
            .Include(ls => ls.Employee)
            .Include(ls => ls.Line)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LineSubscription>> GetSubscriptionsByLineIdAsync(int lineId)
    {
        return await _context.LineSubscriptions
            .Where(ls => ls.LineId == lineId)
            .Include(ls => ls.Employee)
            .Include(ls => ls.Line)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LineSubscription>> GetActiveSubscriptionsAsync()
    {
        var currentDate = DateTime.UtcNow;
        return await _context.LineSubscriptions
            .Where(ls => ls.StartDate <= currentDate && (ls.EndDate == null || ls.EndDate >= currentDate))
            .Include(ls => ls.Employee)
            .Include(ls => ls.Line)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<LineSubscription?> GetLineSubscriptionWithDetailsAsync(int id)
    {
        return await _context.LineSubscriptions
            .Include(ls => ls.Employee) // Eagerly load the related Employee
            .Include(ls => ls.Line)     // Eagerly load the related Line
            .FirstOrDefaultAsync(ls => ls.Id == id);
    }
    
    public async Task<IReadOnlyList<LineSubscription>> GetAllLineSubscriptionsWithDetailsAsync()
    {
        return await _context.LineSubscriptions
            .Include(ls => ls.Employee) // Eagerly load the related Employee
            .Include(ls => ls.Line)     // Eagerly load the related Line
            .AsNoTracking()             // Use for read-only queries
            .ToListAsync();
    }

    public async Task<bool> IsEmployeeSubscribedAsync(int employeeId)
    {
        return await _context.LineSubscriptions
            .AnyAsync(ls => ls.EmployeeId == employeeId && ls.IsActive);
    }

    public async Task<bool> HasAnySubscriptionAsync(int employeeId)
    {
        return await _context.LineSubscriptions
            .AnyAsync(ls => ls.EmployeeId == employeeId);
    }
}