using Data.Entites;
using Domain.Models;

namespace Data.Interfaces;

public interface INotificationRepository : IBaseRepository<NotificationEntity, Notification>
{
    Task DismissNotificationAsync(string userId, string notificationId);
    Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 10);
}