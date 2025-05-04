
using Business.Interfaces;
using Data.Entites;
using Data.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Business.Services;

public class NotificationService(INotificationRepository notificationRepository) : INotificationService
{

    private readonly INotificationRepository _notificationRepository = notificationRepository;
   

    public async Task AddNotificationAsync(NotificationEntity notificationEntity)
    {
        if (string.IsNullOrEmpty(notificationEntity.Icon))
        {
            switch (notificationEntity.NotificationTypeId)
            {
                case 1:
                    notificationEntity.Icon = "~/images/project-img.svg";
                    break;
                case 2:
                    notificationEntity.Icon = "~/images/Avatar-2.svg";
                    break;
                case 3:
                    notificationEntity.Icon = "~/images/avatar-member.svg";
                    break;
            }
        }

        await _notificationRepository.AddAsync(notificationEntity);
        await _notificationRepository.SaveAsync();

    }

    public async Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 20)
    {
        var notifications = await _notificationRepository.GetNotificationsAsync(userId, take);
        return notifications;
    }


    public async Task DismissNotificationAsync(string userId, string notificationId)
    {
        await _notificationRepository.DismissNotificationAsync(userId, notificationId);
    }

}
