using ef_nortwith.dbContext;
using Microsoft.EntityFrameworkCore;

public class RepositorioOrdenes
{
    private readonly NorthwindContext db;

    public RepositorioOrdenes(NorthwindContext db)
    {
        this.db = db;
    }

    public async Task<List<Order>> GetAllOrders(){


            return await db.Orders.Include(e => e.Employee).Include(c => c.Customer).Take(5).ToListAsync();


    }


    public async Task<List<OrderDetail>> GetOrderDetail(int idOrder)
    {

            return await db.OrderDetails.Include(p => p.Product).Where(or => or.OrderId == idOrder).ToListAsync();

    }
 
    
    public async Task<List<Order>> GetAllOrdersDate(DateTime startDate , DateTime endDate)
    {
        //
        return await db.Orders.Where(o => o.RequiredDate >= DateOnly.FromDateTime(startDate) && o.RequiredDate <= DateOnly.FromDateTime(endDate)).ToListAsync();
    } 


}