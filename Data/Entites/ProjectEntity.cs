
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class ProjectEntity
{

    [Key]
    public int Id { get; set; } 

    public string? Image { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(150)")]
    public string Title { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Budget { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; } 
    public virtual ClientEntity Client { get; set; } = null!;

    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public virtual StatusEntity Status { get; set; } = null!;

    public virtual ICollection<ProjectMemberEntity> ProjectMembers { get; set; } = null!;

}
