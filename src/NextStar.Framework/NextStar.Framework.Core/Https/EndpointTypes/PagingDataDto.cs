namespace NextStar.Framework.Core.Https;

public partial class PagingDataDto<T>:IPagingDataDto<T>
{
    public PagingDataDto()
    {
    }

    public PagingDataDto(int total, List<T> data)
    {
        Total = total;
        Data = data;
    }

    public int Total { get; set; }

    public List<T> Data { get; set; }

    /// <summary>
    /// 附加统计数据
    /// </summary>
    public Dictionary<string, int>? AdditionTotal { get; set; } = null;
}