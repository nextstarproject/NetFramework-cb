namespace NextStar.Framework.Core.Https;

public class SortDescriptor : ISortDescriptor
{
    /// <summary>
    ///  排序方向 0:升序  1：降序
    /// </summary>
    public SortDirection Direction { get; set; } = SortDirection.Asc;

    /// <summary>
    ///  属性名
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;
}