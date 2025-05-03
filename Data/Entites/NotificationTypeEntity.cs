
using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class NotificationTypeEntity
{
    [Key]
    public int Id { get; set; }
    public string NewMember { get; set; } = null!;
    public string NewClient { get; set; } = null!;
    public string NewProject { get; set; } = null!;

    public ICollection<NotificationEntity> Notifications { get; set; } = [];

}
