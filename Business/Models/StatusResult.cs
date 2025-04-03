
using Domain.Models;

namespace Business.Models;


public class StatusResult : ServiceResult<Status>
{
    public new IEnumerable<Status>? Result { get; set; }
}

