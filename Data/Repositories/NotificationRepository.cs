
using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class NotificationRepository(DataContext context) : BaseRepository<NotificationEntity, Notification>(context), INotificationRepository
{
  
}
