
using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class TargetGroupEntity
{
    [Key]
    public int Id { get; set; }
    public string All { get; set; } = null!;
    public string Admin { get; set; } = null!;

    public ICollection<NotificationEntity> Notifications { get; set; } = [];

}
