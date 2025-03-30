
namespace Data.Entites;

public class ProjectMemberEntity
{
    public int ProjectId { get; set; }
    public virtual ProjectEntity Project { get; set; } = null!;

    public string MemberId { get; set; } = null!;
    public MemberEntity Member { get; set; } = null!;

}
