using HIAST.Transportation.Application.Models.Identity;

namespace HIAST.Transportation.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(string userId);
    Task<int> GetEmployeeCountAsync();
    Task<List<string>> GetAdminUserIdsAsync();
    Task<bool> IsInRoleAsync(string userId, string roleName);
    public string UserId { get; }
}