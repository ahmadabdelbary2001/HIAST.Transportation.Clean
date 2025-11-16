using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Contracts.Persistence;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<Employee?> GetByEmployeeNumberAsync(string employeeNumber);
    Task<IReadOnlyList<Employee>> GetActiveEmployeesAsync();
    Task<IReadOnlyList<Employee>> GetEmployeesByDepartmentAsync(string department);
}