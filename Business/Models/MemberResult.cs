
using Domain.Models;

namespace Business.Models;

public class MemberResult : ServiceResult<Member>
{
    public new IEnumerable<Member>? Result { get; set; }
}