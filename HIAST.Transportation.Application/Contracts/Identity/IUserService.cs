using HIAST.Transportation.Application.Models.Identity;

namespace HIAST.Transportation.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
    public string UserId { get; }
}