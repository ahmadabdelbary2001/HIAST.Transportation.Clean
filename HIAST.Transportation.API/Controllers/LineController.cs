using HIAST.Transportation.Application.DTOs.Line;
using HIAST.Transportation.Application.Features.Line.Commands.CreateLine;
using HIAST.Transportation.Application.Features.Line.Commands.DeleteLine;
using HIAST.Transportation.Application.Features.Line.Commands.HandoverSupervisor;
using HIAST.Transportation.Application.Features.Line.Commands.UpdateLine;
using HIAST.Transportation.Application.Features.Line.Queries.GetLineDetail;
using HIAST.Transportation.Application.Features.Line.Queries.GetLineList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class LineController : ControllerBase
{
    private readonly IMediator _mediator;

    public LineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Lines
    [HttpGet]
    [ProducesResponseType(typeof(List<LineListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LineListDto>>> Get()
    {
        var lines = await _mediator.Send(new GetLineListQuery());
        return Ok(lines);
    }

    // GET: api/Lines/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LineDto>> Get(int id)
    {
        var line = await _mediator.Send(new GetLineDetailQuery { Id = id });
        return Ok(line);
    }

    // POST: api/Lines
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post([FromBody] CreateLineDto createDto)
    {
        var command = new CreateLineCommand { LineDto = createDto };
        var lineId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = lineId }, lineId);
    }

    // PUT: api/Lines/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateLineDto updateDto)
    {
        // It's good practice to ensure the ID in the route matches the ID in the body.
        if (id != updateDto.Id)
        {
            return BadRequest("ID mismatch between route and body.");
        }

        var command = new UpdateLineCommand { LineDto = updateDto };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/Lines/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLineCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // PUT: api/Line/5/handover
    [HttpPut("{id}/handover")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> HandoverSupervisor(int id, [FromBody] HandoverSupervisorDto handoverDto)
    {
        if (id != handoverDto.LineId)
            return BadRequest("Line ID mismatch");

        // The CommandHandler should verify permissions (IsCurrentSupervisor)
        await _mediator.Send(new HandoverSupervisorCommand { HandoverSupervisorDto = handoverDto });
        return NoContent();
    }
}
