
using Domain.Models;

namespace Business.Models;

public class ClientResult : ServiceResult<Client>
{
    public new IEnumerable<Client>? Result { get; set; }
}
