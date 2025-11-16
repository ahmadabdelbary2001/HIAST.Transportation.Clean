using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(TransportationDbContext context) : base(context)
    {
    }

    public async Task<Employee?> GetByEmployeeNumberAsync(string employeeNumber)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
    }

    public async Task<IReadOnlyList<Employee>> GetActiveEmployeesAsync()
    {
        return await _context.Employees
            .Where(e => e.IsActive)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Employee>> GetEmployeesByDepartmentAsync(string department)
    {
        return await _context.Employees
            .Where(e => e.Department == department)
            .ToListAsync();
    }
}