
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites;

public class MemberAdressEntity
{
    [Key, ForeignKey("Member")]
    public string UserId { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(150)")]
    public string Street { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string City { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Country { get; set; } = null!;
    public virtual MemberEntity Member { get; set; } = null!;
}
