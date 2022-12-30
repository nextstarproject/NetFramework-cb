namespace NextStar.Framework.Core.Https;

public interface ISortDescriptor
{
    /// <summary>
    ///  排序方向 0:升序  1：降序
    /// </summary>
    SortDirection Direction { get; set; }

    /// <summary>
    ///  属性名
    /// </summary>
    string PropertyName { get; set; }
}