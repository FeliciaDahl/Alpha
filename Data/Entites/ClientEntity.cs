
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Data.Entites;

public class ClientEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;

    [ProtectedPersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string ContactPerson { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(150)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Location { get; set; } = null!;
    public string? Phone { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = null!;
}
