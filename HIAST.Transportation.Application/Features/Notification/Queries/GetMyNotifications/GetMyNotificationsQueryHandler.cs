using MediatR;
using HIAST.Transportation.Application.Contracts.Persistence;

namespace HIAST.Transportation.Application.Features.Notification.Queries.GetMyNotifications;

public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQuery, List<Domain.Entities.Notification>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyNotificationsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Domain.Entities.Notification>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.NotificationRepository.GetAllNotificationsByUserIdAsync(request.UserId);
    }
}
