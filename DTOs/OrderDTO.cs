public class OrderDTO
{
    public short OrderId { get; set; }

    public string NameCustomer { get; set; } = "";

    public string NameEmployes { get; set; } = "";

    public DateOnly? OrderDate { get; set; }

    public DateOnly? RequiredDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

}