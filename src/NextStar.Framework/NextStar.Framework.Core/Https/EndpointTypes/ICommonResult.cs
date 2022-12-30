namespace NextStar.Framework.Core.Https;

public interface ICommonResult<T>
{
    public T Data { get; set; }
    public int Code { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
    public Dictionary<string, List<string>>? ErrorMessage { get; set; }
}