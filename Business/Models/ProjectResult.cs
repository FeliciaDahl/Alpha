
using Domain.Models;

namespace Business.Models;

public class ProjectResult : ServiceResult<Project>
{
    public new IEnumerable<Project>? Result { get; set; }
}
