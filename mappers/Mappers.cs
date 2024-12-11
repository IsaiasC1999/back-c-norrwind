using ef_nortwith.dbContext;

public static class Mappers
{


    public static ProductDTO ProducEntityToProductoDTO(Product p)
    {

        return new ProductDTO
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            CompanyName =  p.Category.CategoryName,
            CategoryName = p.Category.CategoryName,
            QuantityPerUnit = p.QuantityPerUnit,
            UnitPrice = p.UnitPrice,
            UnitsInStock = p.UnitsInStock,
            UnitsOnOrder = p.UnitsOnOrder,
            ReorderLevel = p.ReorderLevel,
            Discontinued = p.Discontinued
        };

    }

    public static OrderDTO OrderEntityToOrderDTO(Order order)
    {
        return new OrderDTO
        {
            OrderId = order.OrderId,
            NameCustomer = order.CustomerId,
            NameEmployes = order.EmployeeId.ToString(),
            OrderDate = order.OrderDate,
            RequiredDate = order.RequiredDate,
            ShippedDate = order.ShippedDate,
            ShipAddress = order.ShipAddress,
            ShipCity = order.ShipCity,
            ShipRegion = order.ShipCity,
            ShipPostalCode = order.ShipPostalCode
        };
    }    

    public static List<ProductDTO> ProducEntitiesToProductDTOs(List<Product> products)
    {
        return products.Select(p => new ProductDTO
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            CompanyName =  p.Category.CategoryName,
            CategoryName = p.Category.CategoryName,
            Description = p.Category.Description,
            QuantityPerUnit = p.QuantityPerUnit,
            UnitPrice = p.UnitPrice,
            UnitsInStock = p.UnitsInStock,
            UnitsOnOrder = p.UnitsOnOrder,
            ReorderLevel = p.ReorderLevel,
            Discontinued = p.Discontinued
        }).ToList();
    }


     public static List<OrderListDTO> OrderEntitiesToOrderListDTO(List<Order> orders)
     {


        var orderListDTO = orders.Select( order => new OrderListDTO{

               OrderId = order.OrderId,
               NameCustomer = order.Customer.CustomerId,
               NameEmployes = order.Employee.FirstName + " " +order.Employee.LastName,
               OrderDate = order.OrderDate,
               RequiredDate = order.RequiredDate,
               ShippedDate = order.ShippedDate 

        }).ToList();

           return orderListDTO; 

     }


     public static List<OrderDetailsDTO> OrderDetailEntityToOrderDetailsDTO(List<OrderDetail> orderDetails)
     {
           var orderDetailsDTO = orderDetails.Select( or => new OrderDetailsDTO{
                    UnitPrice = or.UnitPrice,
                    Quantity = or.Quantity,
                    Discount = or.Discount,
                    ProductDTO = Mappers.ProducEntityToProductoDTO(or.Product),
                    
           }).ToList();

           return orderDetailsDTO; 
     }  

     

}


