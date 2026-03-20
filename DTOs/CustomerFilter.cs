namespace ef_nortwith.DTOs;

public class CustomerFilter
{
    public string? CompanyName { get; set; }
    public string? ContactName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
