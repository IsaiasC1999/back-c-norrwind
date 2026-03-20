namespace ef_nortwith.DTOs;

public class CustomerOrdersDTO
{
    public string CustomerId { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public int TotalOrders { get; set; }
    public List<OrderSummaryDTO> Orders { get; set; } = new();
}

public class OrderSummaryDTO
{
    public short OrderId { get; set; }
    public DateOnly? OrderDate { get; set; }
    public DateOnly? RequiredDate { get; set; }
    public DateOnly? ShippedDate { get; set; }
    public string? ShipCountry { get; set; }
    public string? EmployeeName { get; set; }
    public int TotalItems { get; set; }
    public decimal? TotalAmount { get; set; }
}
