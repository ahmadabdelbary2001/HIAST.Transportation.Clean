using HIAST.Transportation.Application.DTOs.Employee;
using HIAST.Transportation.Application.Features.Employee.Commands.CreateEmployee;
using HIAST.Transportation.Application.Features.Employee.Commands.DeleteEmployee;
using HIAST.Transportation.Application.Features.Employee.Commands.UpdateEmployee;
using HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeDetail;
using HIAST.Transportation.Application.Features.Employee.Queries.GetEmployeeList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Employee
    [HttpGet]
    [ProducesResponseType(typeof(List<EmployeeListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeListDto>>> Get()
    {
        var query = new GetEmployeeListQuery();
        var employees = await _mediator.Send(query);
        return Ok(employees);
    }

    // GET: api/Employee/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeDto>> Get(int id)
    {
        var query = new GetEmployeeDetailQuery { Id = id };
        var employee = await _mediator.Send(query);
        return Ok(employee);
    }

    // POST: api/Employee
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post([FromBody] CreateEmployeeDto createDto)
    {
        var command = new CreateEmployeeCommand { EmployeeDto = createDto };
        var employeeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = employeeId }, employeeId);
    }

    // PUT: api/Employee/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateEmployeeDto updateDto)
    {
        // Ensure the ID from the route matches the ID in the body
        if (id != updateDto.Id)
        {
            return BadRequest("ID mismatch between route and body.");
        }

        var command = new UpdateEmployeeCommand { EmployeeDto = updateDto };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/Employee/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteEmployeeCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}

