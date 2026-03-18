using ef_nortwith.dbContext;
using ef_nortwith.DTOs;

public static class Mappers
{

    public static ProductDTO ProducEntityToProductoDTO(Product p)
    {
        return new ProductDTO
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            CompanyName = p.Supplier?.CompanyName ?? "",
            CategoryName = p.Category?.CategoryName ?? "",
            Description = p.Category?.Description ?? "",
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
            NameCustomer = order.Customer?.CompanyName ?? "",
            NameEmployes = order.Employee?.FirstName + " " + order.Employee?.LastName ?? "",
            OrderDate = order.OrderDate,
            RequiredDate = order.RequiredDate,
            ShippedDate = order.ShippedDate,
            ShipAddress = order.ShipAddress,
            ShipCity = order.ShipCity,
            ShipRegion = order.ShipRegion,
            ShipPostalCode = order.ShipPostalCode,
            ShipCountry = order.ShipCountry,
            Freight = order.Freight != null ? (decimal?)order.Freight : null
        };
    }    

    public static List<ProductDTO> ProducEntitiesToProductDTOs(List<Product> products)
    {
        return products.Select(p => new ProductDTO
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            CompanyName = p.Supplier?.CompanyName ?? "",
            CategoryName = p.Category?.CategoryName ?? "",
            Description = p.Category?.Description ?? "",
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
        var orderListDTO = orders.Select(order => new OrderListDTO
        {
            OrderId = order.OrderId,
            NameCustomer = order.Customer?.CompanyName ?? "",
            NameEmployes = (order.Employee?.FirstName ?? "") + " " + (order.Employee?.LastName ?? ""),
            OrderDate = order.OrderDate,
            RequiredDate = order.RequiredDate,
            ShippedDate = order.ShippedDate
        }).ToList();

        return orderListDTO;
    }


    public static EmployeeDTO EmployeeEntityToEmployeeDTO(Employee e)
    {
        var user = e.Users?.FirstOrDefault();
        return new EmployeeDTO
        {
            EmployeeId = e.EmployeeId,
            FirstName = e.FirstName ?? "",
            LastName = e.LastName ?? "",
            Title = e.Title,
            TitleOfCourtesy = e.TitleOfCourtesy,
            BirthDate = e.BirthDate,
            HireDate = e.HireDate,
            Address = e.Address,
            City = e.City,
            Region = e.Region,
            PostalCode = e.PostalCode,
            Country = e.Country,
            HomePhone = e.HomePhone,
            Extension = e.Extension,
            Notes = e.Notes,
            ReportsTo = e.ReportsTo,
            Username = user?.Username,
            Role = user?.Role,
            HasUser = user != null
        };
    }

    public static Employee EmployeeAddDTOToEmployeeEntity(EmployeeAddDTO dto)
    {
        return new Employee
        {
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Title = dto.Title,
            TitleOfCourtesy = dto.TitleOfCourtesy,
            BirthDate = dto.BirthDate,
            HireDate = dto.HireDate,
            Address = dto.Address,
            City = dto.City,
            Region = dto.Region,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            HomePhone = dto.HomePhone,
            Extension = dto.Extension,
            Notes = dto.Notes,
            ReportsTo = dto.ReportsTo
        };
    }

    public static EmployeeDTO EmployeeEntitiToEmpleyeeDTO(Employee e)
    {
        return new EmployeeDTO
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            Title = e.Title,
            HomePhone = e.HomePhone
        };
    }


    public static OrderDetailsDTO OrderDetailEntityToOrderDetailsDTO(OrderDetail orderDetails)
    {
        return new OrderDetailsDTO
        {
            Discount = orderDetails.Discount,
            Quantity = orderDetails.Quantity,
            UnitPrice = orderDetails.UnitPrice,
            ProductDTO = orderDetails.Product != null ? Mappers.ProducEntityToProductoDTO(orderDetails.Product) : null,
            EmployeeDTO = orderDetails.Order?.Employee != null ? Mappers.EmployeeEntitiToEmpleyeeDTO(orderDetails.Order.Employee) : null
        };
    }

    public static List<SuppliersDTO> SuppliersEntityToSuppliersDTO(List<Supplier> supp)
    {
        return supp.Select(s => new SuppliersDTO
        {
            SupplierId = s.SupplierId,
            CompanyName = s.CompanyName,
            ContactName = s.ContactName,
            ContactTitle = s.ContactTitle,
            Address = s.Address,
            City = s.City,
            Region = s.Region,
            PostalCode = s.PostalCode,
            Country = s.Country,
            Phone = s.Phone,
            Fax = s.Fax,
            Homepage = s.Homepage
        }).ToList();
    }


    public static Supplier SupplierDTOtoSupplierEntity(SuppliersDTO s)
    {
        return new Supplier
        {
            SupplierId = s.SupplierId,
            CompanyName = s.CompanyName,
            ContactName = s.ContactName,
            ContactTitle = s.ContactTitle,
            Address = s.Address,
            City = s.City,
            Region = s.Region,
            PostalCode = s.PostalCode,
            Country = s.Country,
            Phone = s.Phone,
            Fax = s.Fax,
            Homepage = s.Homepage
        };
    }


    public static Product ProductDtoByProducEntity(ProducAddDTO p)
    {
        return new Product
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            SupplierId = p.SupplierId,
            CategoryId = p.CategoryId,
            QuantityPerUnit = p.QuantityPerUnit,
            UnitPrice = p.UnitPrice,
            UnitsInStock = p.UnitsInStock,
            UnitsOnOrder = p.UnitsOnOrder,
            ReorderLevel = p.ReorderLevel,
            Discontinued = p.Discontinued
        };
    }

    public static Order OrderAddDTOToOrder(OrderAddDTO dto)
    {
        return new Order
        {
            CustomerId = dto.CustomerId,
            EmployeeId = (short)dto.EmployeeId,
            OrderDate = dto.OrderDate.HasValue ? DateOnly.FromDateTime(dto.OrderDate.Value) : null,
            RequiredDate = dto.RequiredDate.HasValue ? DateOnly.FromDateTime(dto.RequiredDate.Value) : null,
            ShippedDate = dto.ShippedDate.HasValue ? DateOnly.FromDateTime(dto.ShippedDate.Value) : null,
            ShipVia = dto.ShipVia.HasValue ? (short?)dto.ShipVia.Value : null,
            Freight = dto.Freight.HasValue ? (float?)dto.Freight.Value : null,
            ShipName = dto.ShipName,
            ShipAddress = dto.ShipAddress,
            ShipCity = dto.ShipCity,
            ShipRegion = dto.ShipRegion,
            ShipPostalCode = dto.ShipPostalCode,
            ShipCountry = dto.ShipCountry
        };
    }

}


