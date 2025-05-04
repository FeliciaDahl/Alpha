using Business.Interfaces;
using Data.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using WebApp.Hubs;
using WebApp.Services;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(IHubContext<NotificationHub> notificationHub, INotificationService notificationService, INotificationDispatcherService notificationDispatch) : ControllerBase
{
    private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;
    private readonly INotificationService _notificationService = notificationService;
    private readonly INotificationDispatcherService _notificationDispatch = notificationDispatch;

    [HttpPost]
    public async Task<IActionResult> AddNotification([FromBody] NotificationEntity notificationEntity, [FromQuery] string userId)
    {
        await _notificationDispatch.DispatchAsync(notificationEntity, userId);
        return Ok(new { success = true });
    }


    //await _notificationService.AddNotificationAsync(notificationEntity);
    //var notifications = await _notificationService.GetNotificationsAsync("anonymous");
    //var newNotification = notifications.OrderByDescending(x => x.Created).FirstOrDefault();

    //if (newNotification != null)
    //{
    //    await _notificationHub.Clients.All.SendAsync("ReceiveNotification", newNotification);
    //}



    [HttpGet]
    public async Task<IActionResult> GetNotifications()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier ?? "anonymous");
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        var notifications = await _notificationService.GetNotificationsAsync(userId);
        return Ok(notifications);
    }

    [HttpPost("dismiss/{id}")]

    public async Task<IActionResult> DismissNotification(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier ?? "anonymous");
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        await _notificationService.DismissNotificationAsync(userId, id);
        await _notificationHub.Clients.User(userId).SendAsync("NotificationDismissed", id);
        return Ok(new { success = true });
    }
}
