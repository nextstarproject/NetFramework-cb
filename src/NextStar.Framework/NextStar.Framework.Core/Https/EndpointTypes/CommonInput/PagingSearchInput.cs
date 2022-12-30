namespace NextStar.Framework.Core.Https;

public class PagingSearchInput : PagingSortInput, IPagingSearchInput
{
    public string Keyword { get; set; }
}