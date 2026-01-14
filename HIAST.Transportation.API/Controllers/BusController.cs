using HIAST.Transportation.Application.DTOs.Bus;
using HIAST.Transportation.Application.Features.Bus.Commands.CreateBus;
using HIAST.Transportation.Application.Features.Bus.Commands.DeleteBus;
using HIAST.Transportation.Application.Features.Bus.Commands.UpdateBus;
using HIAST.Transportation.Application.Features.Bus.Queries.GetBusDetail;
using HIAST.Transportation.Application.Features.Bus.Queries.GetBusList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class BusController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Bus
    [HttpGet]
    [ProducesResponseType(typeof(List<BusDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BusDto>>> Get()
    {
        var buses = await _mediator.Send(new GetBusListQuery());
        return Ok(buses);
    }

    // GET: api/Bus/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusDto>> Get(int id)
    {
        var bus = await _mediator.Send(new GetBusDetailQuery { Id = id });
        return Ok(bus);
    }

    // POST: api/Bus
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post([FromBody] CreateBusDto createDto)
    {
        var command = new CreateBusCommand { BusDto = createDto };
        var busId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = busId }, busId);
    }

    // PUT: api/Bus/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBusDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID mismatch between route and body.");
        }

        var command = new UpdateBusCommand { BusDto = updateDto };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/Bus/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteBusCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}

