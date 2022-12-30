namespace NextStar.Framework.Core.Https;

public class PagingSortInput : PagingInput, IPagingSortInput
{
    public List<ISortDescriptor> Sorts { get; set; } = new List<ISortDescriptor>();
}