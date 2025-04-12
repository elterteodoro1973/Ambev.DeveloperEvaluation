namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }

}

public class ApiResponseShortData<T> : ApiResponseShort
{
    public T? Data { get; set; }
}

public class ApiResponseShortListData<T> : ApiResponseShort
{
    public List<T>? Data { get; set; }
}