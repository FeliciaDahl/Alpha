
using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class NotificationRepository(DataContext context) : BaseRepository<NotificationEntity, Notification>(context), INotificationRepository
{
  
    public async Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 20)
    {
        var dismissedIds = await _context.DismissedNotifications
            .Where(x => x.UserId == userId)
            .Select(x => x.NotificationId)
            .ToListAsync();

        var notifications = await _context.Notifications
            .Where(x => !dismissedIds.Contains(x.Id))
            .OrderByDescending(x => x.Created)
            .Take(take)
            .ToListAsync();

        return notifications;
    }

    public async Task DismissNotificationAsync(string userId, string notificationId)
    {
        var dismissedNotification = await _context.DismissedNotifications
            .AnyAsync(x => x.UserId == userId && x.NotificationId == notificationId);

        if(!dismissedNotification)
        {
            var dismissed = new NotificationDismissEntity
        {
            UserId = userId,
            NotificationId = notificationId
        };

            _context.DismissedNotifications.Add(dismissed);
            await _context.SaveChangesAsync();
        }
       
    }
}
