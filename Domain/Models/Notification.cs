
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Notification
{
    public int TargetGroupId { get; set; }
    public int NotificationTypeId { get; set; }
    public string Icon { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;


}
