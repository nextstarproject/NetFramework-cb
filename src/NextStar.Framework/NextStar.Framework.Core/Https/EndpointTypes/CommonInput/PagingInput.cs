namespace NextStar.Framework.Core.Https;

public class PagingInput : IPagingInput
{
    public int PageSize { get; set; } = 50;

    public int PageIndex { get; set; } = 1;
}