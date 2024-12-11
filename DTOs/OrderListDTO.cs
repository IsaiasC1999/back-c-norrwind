public class OrderListDTO
{

    public short OrderId { get; set; }

    public string NameCustomer { get; set; } = "";

    public string NameEmployes { get; set; } = "";

    public DateOnly? OrderDate { get; set; }

    public DateOnly? RequiredDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    
}