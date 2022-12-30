namespace NextStar.Framework.Core.Https;

public interface IPagingDataDto<T>
{
    public int Total { get; set; }

    public List<T> Data { get; set; }

    /// <summary>
    /// 附加统计数据
    /// </summary>
    public Dictionary<string, int>? AdditionTotal { get; set; }
}
