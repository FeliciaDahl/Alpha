using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class NotificationHub : Hub
{

    public async Task SendNotificationToAll(object notification)
    {
        await Clients.All.SendAsync("ReceiveNotification", notification);
    }
    public async Task SendNotificationToAdmins(object notification)
    {
        await Clients.All.SendAsync("AdminReceiveNotification", notification);
    }

    public async Task DismissNotification(string notificationId)
    {
        await Clients.All.SendAsync("DismissNotification", notificationId);
    }

}
