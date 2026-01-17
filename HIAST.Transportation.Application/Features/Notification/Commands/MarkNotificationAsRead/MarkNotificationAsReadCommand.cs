using MediatR;

namespace HIAST.Transportation.Application.Features.Notification.Commands.MarkNotificationAsRead;

public record MarkNotificationAsReadCommand(int NotificationId, string UserId) : IRequest<Unit>;
