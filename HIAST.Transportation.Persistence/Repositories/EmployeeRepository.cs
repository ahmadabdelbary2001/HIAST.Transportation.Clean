using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Domain.Enums;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<Employee?> GetByEmployeeIdAsync(string employeeId)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeId);
    }

    public async Task<IReadOnlyList<Employee>> GetEmployeesByDepartmentAsync(string department)
    {
        // Safely parse the string into a Department enum value.
        // The 'true' argument makes the parsing case-insensitive.
        if (Enum.TryParse<Department>(department, true, out var departmentEnum))
        {
            return await _context.Employees
                .Where(e => e.Department == departmentEnum)
                .ToListAsync();
        }

        // If the string is not a valid department, return an empty list.
        return new List<Employee>();
    }
    
    public async Task<Employee?> GetEmployeeWithSubscriptionDetailsAsync(int employeeId)
    {
        return await _context.Employees
            .Include(e => e.Subscription)
            .ThenInclude(sub => sub.Line)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == employeeId);
    }
}