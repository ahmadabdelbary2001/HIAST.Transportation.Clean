using HIAST.Transportation.Application.DTOs.Supervisor;
using HIAST.Transportation.Application.Features.Supervisor.Queries.GetSupervisorLineAssignments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class SupervisorController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupervisorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Supervisor/LineAssignments
    [HttpGet("LineAssignments")]
    [ProducesResponseType(typeof(IReadOnlyList<SupervisorLineDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SupervisorLineDto>>> GetLineAssignments()
    {
        var query = new GetSupervisorLineAssignmentsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
