using HIAST.Transportation.Application.DTOs.Driver;
using HIAST.Transportation.Application.Features.Driver.Commands.CreateDriver;
using HIAST.Transportation.Application.Features.Driver.Commands.DeleteDriver;
using HIAST.Transportation.Application.Features.Driver.Commands.UpdateDriver;
using HIAST.Transportation.Application.Features.Driver.Queries.GetDriverList;
using HIAST.Transportation.Application.Features.Driver.Queries.GetDriverDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Driver
    [HttpGet]
    [ProducesResponseType(typeof(List<DriverDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DriverDto>>> Get()
    {
        var drivers = await _mediator.Send(new GetDriverListQuery());
        return Ok(drivers);
    }

    // GET: api/Driver/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DriverDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DriverDto>> Get(int id)
    {
        var driver = await _mediator.Send(new GetDriverDetailQuery { Id = id });
        return Ok(driver);
    }

    // POST: api/Driver
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post([FromBody] CreateDriverDto createDto)
    {
        var command = new CreateDriverCommand { DriverDto = createDto };
        var driverId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = driverId }, driverId);
    }

    // PUT: api/Driver/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateDriverDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID mismatch between route and body.");
        }

        var command = new UpdateDriverCommand { DriverDto = updateDto };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/Driver/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteDriverCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}

