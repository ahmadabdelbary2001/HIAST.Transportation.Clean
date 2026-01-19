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
            FirstName = employee?.FirstName,
            LastName = employee?.LastName,
            UserName = employee?.UserName,
            PhoneNumber = employee?.PhoneNumber,
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
            FirstName = q.FirstName,
            LastName = q.LastName,
            UserName = q.UserName,
            PhoneNumber = q.PhoneNumber,
            EmployeeNumber = q.EmployeeNumber,
            Department = q.Department
        }).ToList();
    }

    public async Task UpdateEmployee(Employee employee)
    {
        var user = await _userManager.FindByIdAsync(employee.Id);
        if (user != null)
        {
            user.FirstName = employee.FirstName;
            user.LastName = employee.LastName;
            user.UserName = employee.UserName; 
            user.PhoneNumber = employee.PhoneNumber;
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

    public async Task<int> GetEmployeeCountAsync()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Count;
    }

    public async Task<List<string>> GetAdminUserIdsAsync()
    {
        var admins = await _userManager.GetUsersInRoleAsync("Administrator");
        return admins.Select(u => u.Id).ToList();
    }

    public async Task<bool> IsInRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;
        
        return await _userManager.IsInRoleAsync(user, roleName);
    }
}