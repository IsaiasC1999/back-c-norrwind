using ef_nortwith.dbContext;
using ef_nortwith.DTOs;

namespace ef_nortwith.interfacez;

public interface IRepositorioOrdenes
{
    Task<List<Order>> GetAllOrders();
    Task<(List<Order> Orders, int TotalCount)> GetOrdersPaginated(OrderFilter filter, int offset, int limit);
    Task<Order?> GetOrderById(int id);
    Task<OrderDetail?> GetOrderDetail(int idOrder);
    Task<List<Order>> GetOrdersByDate(DateTime startDate, DateTime endDate);
    Task<bool> CreateOrder(Order order);
    Task<bool> UpdateOrder(Order order);
    Task<bool> DeleteOrder(int id);
}
