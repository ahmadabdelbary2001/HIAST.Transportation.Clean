using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<Employee?> GetByEmployeeIdAsync(string employeeId);
    Task<IReadOnlyList<Employee>> GetEmployeesByDepartmentAsync(string department);
}