using Data.Models;

namespace Business.Models;

public  class ServiceResult<T>
{
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }

    public T? Result { get; set; }



    public static ServiceResult<T> Success(T result)
    {
        return new ServiceResult<T>
        {
            Succeeded = true,
            StatusCode = 200,
            Result = result
        };
    }

    public static ServiceResult<T> Failed(int statusCode, string error)
    {
        return new ServiceResult<T>
        {
            Succeeded = false,
            StatusCode = statusCode,
            Error = error
        };
    }

}
