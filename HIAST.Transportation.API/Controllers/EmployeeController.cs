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

    public EmployeeController(Application.Contracts.Identity.IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/Employee
    [HttpGet]
    [ProducesResponseType(typeof(List<HIAST.Transportation.Application.Models.Identity.Employee>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<HIAST.Transportation.Application.Models.Identity.Employee>>> Get()
    {
        var employees = await _userService.GetEmployees();
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

