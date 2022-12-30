namespace NextStar.Framework.Core.Https;

public interface IPagingInput
{
    int PageSize { get; set; }
    int PageIndex { get; set; }
}
