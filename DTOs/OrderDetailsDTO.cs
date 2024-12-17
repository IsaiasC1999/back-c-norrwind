using ef_nortwith.dbContext;

public class OrderDetailsDTO
{   
    public float UnitPrice { get; set; }

    public short Quantity { get; set; }

    public float Discount { get; set; }

    public virtual ProductDTO ProductDTO { get; set; } = null!;

    public virtual EmployeeDTO EmployeeDTO { get; set; } = null!;
    
}