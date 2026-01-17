using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Application.Features.Notification.Commands.MarkNotificationAsRead;
using HIAST.Transportation.Application.Features.Notification.Queries.GetMyNotifications;
using HIAST.Transportation.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public NotificationController(IMediator mediator, IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Notification>>> GetMyNotifications()
    {
        var notifications = await _mediator.Send(new GetMyNotificationsQuery(_userService.UserId));
        return Ok(notifications);
    }

    [HttpPut("{id}/read")]
    public async Task<ActionResult> MarkAsRead(int id)
    {
        await _mediator.Send(new MarkNotificationAsReadCommand(id, _userService.UserId));
        return NoContent();
    }
}
