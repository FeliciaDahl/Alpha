
namespace Data.Models;

public class RepositoryResults<T>
{
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public T? Result { get; set; }

    public static RepositoryResults<T> Success(T result)
    {
        return new RepositoryResults<T>
        {
            Succeeded = true,
            StatusCode = 200,
            Result = result
        };
    }

    public static RepositoryResults<T> Failed(int statusCode, string error)
    {
        return new RepositoryResults<T>
        {
            Succeeded = false,
            StatusCode = statusCode,
            Error = error
        };
    }

}
