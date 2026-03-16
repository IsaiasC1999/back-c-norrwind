using ef_nortwith.dbContext;
using ef_nortwith.DTOs;

namespace ef_nortwith.interfacez;

public interface IRepositorioOrdenes
{
    Task<List<Order>> GetAllOrders();
    Task<List<Order>> GetOrders(OrderFilter filter);
    Task<Order?> GetOrderById(int id);
    Task<OrderDetail?> GetOrderDetail(int idOrder);
    Task<List<Order>> GetOrdersByDate(DateTime startDate, DateTime endDate);
    Task<bool> CreateOrder(Order order);
    Task<bool> UpdateOrder(Order order);
    Task<bool> DeleteOrder(int id);
}
