using System.Security.Claims;
using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Application.Models.Identity;
using HIAST.Transportation.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HIAST.Transportation.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public string UserId
    {
        get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid");
    }

    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);
        return new Employee
        {
            Email = employee?.Email,
            Id = employee?.Id,
            Firstname = employee?.FirstName,
            Lastname = employee?.LastName,
            EmployeeNumber = employee?.EmployeeNumber,
            Department = employee?.Department
        };
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(q => new Employee
        {
            Id = q.Id,
            Email = q.Email,
            Firstname = q.FirstName,
            Lastname = q.LastName,
            EmployeeNumber = q.EmployeeNumber,
            Department = q.Department
        }).ToList();
    }

    public async Task UpdateEmployee(Employee employee)
    {
        var user = await _userManager.FindByIdAsync(employee.Id);
        if (user != null)
        {
            user.FirstName = employee.Firstname;
            user.LastName = employee.Lastname;
            user.EmployeeNumber = employee.EmployeeNumber;
            user.Department = employee.Department;

            await _userManager.UpdateAsync(user);
        }
    }

    public async Task DeleteEmployee(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
    }
}