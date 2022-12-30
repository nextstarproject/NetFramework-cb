using System.Net;

namespace NextStar.Framework.Core.Https;

public partial class CommonResult<T> : ICommonResult<T>
{
    public T Data { get; set; } = default(T);
    public int Code { get; set; } = (int)HttpStatusCode.OK;
    public string? Message { get; set; } = null;
    public bool Success { get; set; } = false;
    public Dictionary<string, List<string>>? ErrorMessage { get; set; } = null;

    public CommonResult()
    {
        Code = (int)HttpStatusCode.OK;
        Success = true;
    }

    public CommonResult(T data, string? message = null, int code = (int)HttpStatusCode.OK, bool success = true)
    {
        Data = data;
        Code = code;
        Message = message;
        Success = success;
    }
}

public partial class CommonResult
{
    public static ICommonResult<T> From<T>(T data)
    {
        return new CommonResult<T>(data);
    }
    public static ICommonResult<T> BadRequest<T>(T data, string? message = null)
    {
        return new CommonResult<T>(data, message.IsNullOrWhiteSpace() ? HttpStatusCode.BadRequest.ToString() : message, (int)HttpStatusCode.BadRequest, false);
    }
}