namespace ef_nortwith.DTOs;

public class ProductFilter
{
    public string? Category { get; set; }
    public float? PriceMin { get; set; }
    public float? PriceMax { get; set; }
    public string? Supplier { get; set; }
}
