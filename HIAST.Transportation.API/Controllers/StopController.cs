using HIAST.Transportation.Application.DTOs.Stop;
using HIAST.Transportation.Application.Features.Stop.Commands.CreateStop;
using HIAST.Transportation.Application.Features.Stop.Commands.DeleteStop;
using HIAST.Transportation.Application.Features.Stop.Commands.UpdateStop;
using HIAST.Transportation.Application.Features.Stop.Queries.GetStopDetail;
using HIAST.Transportation.Application.Features.Stop.Queries.GetStopList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class StopController : ControllerBase
{
    private readonly IMediator _mediator;

    public StopController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Stops
    [HttpGet]
    [ProducesResponseType(typeof(List<StopListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StopListDto>>> Get()
    {
        var stops = await _mediator.Send(new GetStopListQuery());
        return Ok(stops);
    }

    // GET: api/Stops/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(StopDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StopDto>> Get(int id)
    {
        var stop = await _mediator.Send(new GetStopDetailQuery { Id = id });
        return Ok(stop);
    }

    // POST: api/Stops
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post([FromBody] CreateStopDto createDto)
    {
        var command = new CreateStopCommand { StopDto = createDto };
        var stopId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = stopId }, stopId);
    }

    // PUT: api/Stops/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateStopDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID mismatch between route and body.");
        }

        var command = new UpdateStopCommand { StopDto = updateDto };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/Stops/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteStopCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // PUT: api/Stop/reorder
    [HttpPut("reorder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReorderStops([FromBody] Application.DTOs.Stop.ReorderStopsDto dto)
    {
        var command = new Application.Features.Stop.Commands.ReorderStops.ReorderStopsCommand 
        { 
            ReorderDto = dto 
        };
        await _mediator.Send(command);
        return NoContent();
    }
}

