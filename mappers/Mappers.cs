using ef_nortwith.dbContext;

public static class Mappers
{


    public static ProductDTO ProducEntityToProductoDTO(Product p)
    {

        return new ProductDTO
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            // CompanyName =  p.Supplier.CompanyName,
            // CategoryName = p.Category.CategoryName,
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
            CompanyName =  p.Supplier.CompanyName,
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


     public static EmployeeDTO EmployeeEntitiToEmpleyeeDTO(Employee e)
     {

         return new EmployeeDTO{
              FirstName = e.FirstName,
              LastName = e.LastName,
              Title = e.Title,
              HomePhone = e.HomePhone  
         };

     }   


     public static OrderDetailsDTO OrderDetailEntityToOrderDetailsDTO(OrderDetail orderDetails)
     {
          

           return new OrderDetailsDTO{
                 Discount = orderDetails.Discount,
                 Quantity = orderDetails.Quantity,
                 UnitPrice = orderDetails.UnitPrice,
                 ProductDTO = Mappers.ProducEntityToProductoDTO(orderDetails.Product),
                EmployeeDTO = Mappers.EmployeeEntitiToEmpleyeeDTO(orderDetails.Order.Employee)
           }; 
     }  

     public static List<SuppliersDTO> SuppliersEntityToSuppliersDTO(List<Supplier> supp)
     {
           return supp.Select( s => new SuppliersDTO
           {
              SupplierId = s.SupplierId,
              CompanyName = s.CompanyName,
              ContactName = s.ContactName,
              ContactTitle = s.ContactTitle,
              Address = s.Address,
              City = s.City,
              Region = s.Region,
              PostalCode =s.PostalCode,
              Country = s.Country,
              Phone = s.Phone,
              Fax = s.Fax,
              Homepage = s.Homepage  
           }).ToList();
     }


    public static Supplier SupplierDTOtoSupplierEntity(SuppliersDTO s)
    {
        return new Supplier{
              SupplierId = s.SupplierId,
              CompanyName = s.CompanyName,
              ContactName = s.ContactName,
              ContactTitle = s.ContactTitle,
              Address = s.Address,
              City = s.City,
              Region = s.Region,
              PostalCode =s.PostalCode,
              Country = s.Country,
              Phone = s.Phone,
              Fax = s.Fax,
              Homepage = s.Homepage  
        };
    }
}


