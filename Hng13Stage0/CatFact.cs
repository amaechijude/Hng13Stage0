namespace Hng13Stage0;

internal class CatFact
{
    public string? Fact { get; set; }
    public int Length { get; set; }
    public static CatFact Fallback => new() { Fact = "Failed to fetch random fact", Length = 0 };
}