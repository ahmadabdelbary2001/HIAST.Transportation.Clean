using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly Application.Contracts.Identity.IUserService _userService;
    private readonly Application.Contracts.Persistence.IUnitOfWork _unitOfWork;

    public EmployeeController(Application.Contracts.Identity.IUserService userService, Application.Contracts.Persistence.IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
    }

    // GET: api/Employee
    [HttpGet]
    [ProducesResponseType(typeof(List<HIAST.Transportation.Application.Models.Identity.Employee>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<HIAST.Transportation.Application.Models.Identity.Employee>>> Get()
    {
        var employees = await _userService.GetEmployees();
        
        // Hydrate with subscription info
        foreach (var employee in employees)
        {
             if (!string.IsNullOrEmpty(employee.Id))
             {
                 var subscriptions = await _unitOfWork.LineSubscriptionRepository.GetSubscriptionsByEmployeeIdAsync(employee.Id);
                 var activeSubscription = subscriptions.FirstOrDefault(s => s.IsActive);
                 if (activeSubscription != null)
                 {
                     employee.LineSubscriptionId = activeSubscription.Id;
                     employee.SubscribedLineId = activeSubscription.LineId;
                     employee.IsSubscriptionActive = true;

                     // Fetch Line Name
                     var line = await _unitOfWork.LineRepository.GetByIdAsync(activeSubscription.LineId);
                     employee.SubscribedLineName = line?.Name;
                 }
             }
        }

        return Ok(employees);
    }

    // GET: api/Employee/detail?userId=xxx
    [HttpGet("detail")]
    [ProducesResponseType(typeof(HIAST.Transportation.Application.Models.Identity.Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HIAST.Transportation.Application.Models.Identity.Employee>> GetByUserId([FromQuery] string userId)
    {
        var employee = await _userService.GetEmployee(userId);
        if (employee == null) return NotFound();

        // Hydrate with subscription info
        var subscriptions = await _unitOfWork.LineSubscriptionRepository.GetSubscriptionsByEmployeeIdAsync(userId);
        var activeSubscription = subscriptions.FirstOrDefault(s => s.IsActive);
        if (activeSubscription != null)
        {
            employee.LineSubscriptionId = activeSubscription.Id;
            employee.SubscribedLineId = activeSubscription.LineId;
            employee.IsSubscriptionActive = true;

            var line = await _unitOfWork.LineRepository.GetByIdAsync(activeSubscription.LineId);
            employee.SubscribedLineName = line?.Name;
        }

        return Ok(employee);
    }

    // POST: api/Employee
    // Note: Use Register endpoint usually, but if exposed:
    // This is problematic without a specific Create method in IUserService distinct from Register.
    // IUserService typically manages existing. Let's assume Register is used for creation. 
    // I will comment out Post/Delete if IUserService doesn't support them fully or rely on AuthController.
    // However, IUserService HAS UpdateEmployee and DeleteEmployee.
    
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] HIAST.Transportation.Application.Models.Identity.Employee employee)
    {
        await _userService.UpdateEmployee(employee);
        return NoContent();
    }

    // DELETE: api/Employee/detail?userId=xxx
    [HttpDelete("detail")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByUserId([FromQuery] string userId)
    {
        await _userService.DeleteEmployee(userId);
        return NoContent();
    }
}

