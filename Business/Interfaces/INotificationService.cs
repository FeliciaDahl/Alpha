using Data.Entites;

namespace Business.Interfaces
{
    public interface INotificationService
    {
        Task AddNotificationAsync(NotificationEntity notificationEntity);
        Task DismissNotificationAsync(string userId, string notificationId);
        Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 10);
    }
}