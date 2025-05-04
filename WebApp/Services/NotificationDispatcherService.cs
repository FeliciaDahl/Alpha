using Business.Interfaces;
using Data.Entites;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace WebApp.Services;

public interface INotificationDispatcherService
{
    Task DispatchAsync(NotificationEntity notificationEntity, string userId);
}

public class NotificationDispatcherService(INotificationService notificationService, IHubContext<NotificationHub> hubContext) : INotificationDispatcherService
{
    private readonly INotificationService _notificationService = notificationService;
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task DispatchAsync(NotificationEntity notificationEntity, string userId)
    {
        await _notificationService.AddNotificationAsync(notificationEntity);

        var notifications = await _notificationService.GetNotificationsAsync(userId);
        var newNotification = notifications.OrderByDescending(x => x.Created).FirstOrDefault();

        if (newNotification != null)
        {
            if (notificationEntity.TargetGroupId == 2)
            {
                await _hubContext.Clients.All.SendAsync("AdminReceiveNotification", newNotification);
            }
            else
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", newNotification);
            }
        }
    }
}
