using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;

public class OrdenesServices
{
    private readonly IRepositorioOrdenes repositorioOrdenes;

    public OrdenesServices(IRepositorioOrdenes repositorioOrdenes)
    {
        this.repositorioOrdenes = repositorioOrdenes;
    }

    public async Task<ResponseServices> GetAllOrders()
    {
        var orders = await repositorioOrdenes.GetAllOrders();

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.OrderEntitiesToOrderListDTO(orders),
            Error = ""
        };
    }

    public async Task<PaginatedResponse<OrderListDTO>> GetOrdersPaginated(OrderFilter filter, int offset, int limit)
    {
        var (orders, totalCount) = await repositorioOrdenes.GetOrdersPaginated(filter, offset, limit);

        var totalPages = (int)Math.Ceiling((double)totalCount / limit);

        return new PaginatedResponse<OrderListDTO>
        {
            Success = true,
            Result = Mappers.OrderEntitiesToOrderListDTO(orders),
            Pagination = new PaginationMeta
            {
                Offset = offset,
                Limit = limit,
                TotalRecords = totalCount,
                TotalPages = totalPages
            },
            Error = ""
        };
    }

    public async Task<ResponseServices> GetOrderById(int id)
    {
        var order = await repositorioOrdenes.GetOrderById(id);

        if (order == null)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "La orden no existe",
                Result = null
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.OrderEntityToOrderDTO(order),
            Error = ""
        };
    }

    public async Task<ResponseServices> GetOrderDetail(int idOrder)
    {
        var orderDetail = await repositorioOrdenes.GetOrderDetail(idOrder);

        if (orderDetail == null)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "No se encontró el detalle de la orden",
                Result = null
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.OrderDetailEntityToOrderDetailsDTO(orderDetail),
            Error = ""
        };
    }

    public async Task<ResponseServices> GetOrdersByDate(DateTime startDate, DateTime endDate)
    {
        var orders = await repositorioOrdenes.GetOrdersByDate(startDate, endDate);

        if (orders == null || orders.Count == 0)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "No se encontraron órdenes en el rango de fechas especificado",
                Result = null
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.OrderEntitiesToOrderListDTO(orders),
            Error = ""
        };
    }

    public async Task<ResponseServices> CreateOrder(OrderAddDTO orderDTO)
    {
        var order = Mappers.OrderAddDTOToOrder(orderDTO);
        
        var result = await repositorioOrdenes.CreateOrder(order);

        if (!result)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "Error al crear la orden. Verifique que el cliente y empleado existan.",
                Result = null
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = "Orden creada correctamente",
            Error = ""
        };
    }

    public async Task<ResponseServices> UpdateOrder(int id, OrderAddDTO orderDTO)
    {
        var existingOrder = await repositorioOrdenes.GetOrderById(id);
        
        if (existingOrder == null)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "La orden no existe",
                Result = null
            };
        }

        var order = Mappers.OrderAddDTOToOrder(orderDTO);
        order.OrderId = (short)id;

        var result = await repositorioOrdenes.UpdateOrder(order);

        if (!result)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "Error al actualizar la orden",
                Result = null
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = "Orden actualizada correctamente",
            Error = ""
        };
    }

    public async Task<ResponseServices> DeleteOrder(int id)
    {
        var result = await repositorioOrdenes.DeleteOrder(id);

        if (!result)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "La orden no existe o no se pudo eliminar",
                Result = null
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = "Orden eliminada correctamente",
            Error = ""
        };
    }
}
