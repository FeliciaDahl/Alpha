
namespace Data.Entites;

public class ProjectMemberEntity
{
    public string ProjectId { get; set; } = null!;
    public virtual ProjectEntity Project { get; set; } = null!;

    public string MemberId { get; set; } = null!;
    public MemberEntity Member { get; set; } = null!;

}
