using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;
using Microsoft.EntityFrameworkCore;

public class RepositorioOrdenes : IRepositorioOrdenes
{
    private readonly NorthwindContext db;

    public RepositorioOrdenes(NorthwindContext db)
    {
        this.db = db;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await db.Orders
            .Include(e => e.Employee)
            .Include(c => c.Customer)
            .ToListAsync();
    }

    public async Task<(List<Order> Orders, int TotalCount)> GetOrdersPaginated(OrderFilter filter, int offset, int limit)
    {
        var query = db.Orders
            .Include(e => e.Employee)
            .Include(c => c.Customer)
            .AsQueryable();

        if (filter != null)
        {
            if (filter.OrderId.HasValue)
            {
                query = query.Where(o => o.OrderId == filter.OrderId.Value);
            }

            if (!string.IsNullOrEmpty(filter.Cliente))
            {
                query = query.Where(o => o.Customer != null && o.Customer.CompanyName.Contains(filter.Cliente));
            }

            if (filter.FechaInicio.HasValue)
            {
                query = query.Where(o => o.OrderDate >= DateOnly.FromDateTime(filter.FechaInicio.Value));
            }

            if (filter.FechaFin.HasValue)
            {
                query = query.Where(o => o.OrderDate <= DateOnly.FromDateTime(filter.FechaFin.Value));
            }

            if (filter.EmpleadoId.HasValue)
            {
                query = query.Where(o => o.EmployeeId == filter.EmpleadoId.Value);
            }
        }

        var totalCount = await query.CountAsync();
        
        var orders = await query
            .OrderByDescending(o => o.OrderId)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return (orders, totalCount);
    }

    public async Task<Order?> GetOrderById(int id)
    {
        return await db.Orders
            .Include(e => e.Employee)
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<OrderDetail?> GetOrderDetail(int idOrder)
    {
        return await db.OrderDetails
            .Include(p => p.Product)
            .Include(e => e.Order.Employee)
            .FirstOrDefaultAsync(or => or.OrderId == idOrder);
    }

    public async Task<List<Order>> GetOrdersByDate(DateTime startDate, DateTime endDate)
    {
        return await db.Orders
            .Include(e => e.Employee)
            .Include(c => c.Customer)
            .Where(o => o.RequiredDate >= DateOnly.FromDateTime(startDate) && o.RequiredDate <= DateOnly.FromDateTime(endDate))
            .ToListAsync();
    }

    public async Task<bool> CreateOrder(Order order)
    {
        try
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        var existingOrder = await db.Orders.FindAsync(order.OrderId);
        
        if (existingOrder == null)
        {
            return false;
        }

        db.Entry(existingOrder).CurrentValues.SetValues(order);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var order = await db.Orders.FindAsync(id);
        
        if (order == null)
        {
            return false;
        }

        db.Orders.Remove(order);
        await db.SaveChangesAsync();
        return true;
    }
}
