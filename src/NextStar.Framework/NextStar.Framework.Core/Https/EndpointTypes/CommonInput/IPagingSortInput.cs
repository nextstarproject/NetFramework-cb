namespace NextStar.Framework.Core.Https;

public interface IPagingSortInput
{
    List<ISortDescriptor> Sorts { get; set; }
}