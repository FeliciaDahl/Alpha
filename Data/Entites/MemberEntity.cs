
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entites;

public class MemberEntity : IdentityUser
{
    [ProtectedPersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string? JobTitle { get; set; } 


public virtual MemberAdressEntity? MemberAdress { get; set; } = null!;

public virtual ICollection<ProjectMemberEntity> ProjectMembers { get; set; } = null!;
}
