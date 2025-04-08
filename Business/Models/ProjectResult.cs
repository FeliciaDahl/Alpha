
using Domain.Models;

namespace Business.Models;

public class ProjectResult<T> : ServiceResult<Project>
{
    public T? Result { get; set; }
}
public class ProjectResult : ServiceResult
{

}