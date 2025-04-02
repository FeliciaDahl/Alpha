
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

[Index(nameof(StatusName), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public string Id { get; set; } = null!;
    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = null!;
}
