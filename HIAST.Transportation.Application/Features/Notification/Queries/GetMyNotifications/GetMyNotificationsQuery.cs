using MediatR;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.Features.Notification.Queries.GetMyNotifications;

public record GetMyNotificationsQuery(string UserId) : IRequest<List<Domain.Entities.Notification>>;
