using HIAST.Transportation.Application.DTOs.LineSubscription;
using HIAST.Transportation.Application.Features.LineSubscription.Commands.CreateLineSubscription;
using HIAST.Transportation.Application.Features.LineSubscription.Commands.DeleteLineSubscription;
using HIAST.Transportation.Application.Features.LineSubscription.Commands.UpdateLineSubscription;
using HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionDetail;
using HIAST.Transportation.Application.Features.LineSubscription.Queries.GetLineSubscriptionList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LineSubscriptionController : ControllerBase
{
    private readonly IMediator _mediator;

    public LineSubscriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/LineSubscriptions
    [HttpGet]
    [ProducesResponseType(typeof(List<LineSubscriptionListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LineSubscriptionListDto>>> Get()
    {
        var subscriptions = await _mediator.Send(new GetLineSubscriptionListQuery());
        return Ok(subscriptions);
    }

    // GET: api/LineSubscriptions/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LineSubscriptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LineSubscriptionDto>> Get(int id)
    {
        var subscription = await _mediator.Send(new GetLineSubscriptionDetailQuery { Id = id });
        return Ok(subscription);
    }

    // POST: api/LineSubscriptions
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post([FromBody] CreateLineSubscriptionDto createDto)
    {
        var command = new CreateLineSubscriptionCommand { LineSubscriptionDto = createDto };
        var subscriptionId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = subscriptionId }, subscriptionId);
    }

    // PUT: api/LineSubscriptions/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateLineSubscriptionDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID mismatch between route and body.");
        }

        var command = new UpdateLineSubscriptionCommand { LineSubscriptionDto = updateDto };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/LineSubscriptions/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLineSubscriptionCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
